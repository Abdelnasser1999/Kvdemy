using Kvdemy.Infrastructure.Services.Interfaces;
using Kvdemy.Data.Models;
using Kvdemy.Infrastructure.Middlewares;
using Kvdemy.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using FirebaseAdmin.Messaging;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Kvdemy.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<KvdemyDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IInterfaceServices, InterfaceServices>();
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(1); // You can set the time
});
builder.Services.AddMemoryCache();
builder.Services.AddRazorPages();

// Add Identity services
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<KvdemyDbContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings (customize as per requirements)
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;

    // User settings
    options.User.RequireUniqueEmail = true;
});

// Removed AddAuthentication call as AddIdentity already configures authentication

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", builder =>
    {
        builder.WithOrigins("http://localhost:3000")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});
builder.Services.AddLocalization();
builder.Services.AddScoped<FirebaseMessaging>(provider =>
{
    return FirebaseMessaging.DefaultInstance;
});
builder.Services.AddScoped<IFileService, FileService>();

if (FirebaseApp.DefaultInstance == null)
{
    FirebaseApp.Create(new AppOptions()
    {
        Credential = GoogleCredential.FromFile("Credentials/serviceAccountKey.json")
    });
}

builder.Services.Configure<RequestLocalizationOptions>(opts =>
{
    var supportedCultures = new List<CultureInfo>
    {
        new CultureInfo("en-US"),
        new CultureInfo("en"),
        new CultureInfo("ar-SA"),
        new CultureInfo("ar")
    };

    opts.DefaultRequestCulture = new RequestCulture("en-US");
    opts.SupportedCultures = new List<CultureInfo>
    {
        new CultureInfo("en-US"),
        new CultureInfo("en")
    };
    opts.SupportedUICultures = supportedCultures;
    opts.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(context =>
    {
        var defaultLang = "en-US";
        var userLanguages = context.Request.GetTypedHeaders().AcceptLanguage;
        if (userLanguages.Any() && !string.IsNullOrWhiteSpace(userLanguages.FirstOrDefault()?.ToString()))
        {
            var passedLanguage = userLanguages.FirstOrDefault()?.ToString();
            if (!string.IsNullOrWhiteSpace(passedLanguage) &&
                passedLanguage.StartsWith("ar", StringComparison.OrdinalIgnoreCase))
            {
                defaultLang = "ar-SA";
            }
        }

        return Task.FromResult(new ProviderCultureResult(defaultLang));
    }));
});

builder.Services.AddAutoMapper(typeof(Kvdemy.infrastructure.Mapper.AutoMapper).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRequestLocalization();
app.UseRouting();

// Use middleware exception handler
app.UseExceptionHandler(options => options.UseMiddleware<ExceptionHandler>());

app.UseAuthentication(); // Ensure this is present
app.UseAuthorization();  // Ensure this is present

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

// Seed the admin user
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<User>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    var adminEmail = "admin@kvdemy.com";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        adminUser = new User { UserName = adminEmail, Email = adminEmail };
        await userManager.CreateAsync(adminUser, "AdminPassword123!");

        // Ensure the "Admin" role exists before assigning it to the user
        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        await userManager.AddToRoleAsync(adminUser, "Admin");
    }
}

app.Run();
