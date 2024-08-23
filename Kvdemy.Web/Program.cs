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
    options.IdleTimeout = TimeSpan.FromMinutes(1);//You can set Time
});
builder.Services.AddIdentity<User, IdentityRole>(config =>
{

    config.User.RequireUniqueEmail = false;
    config.Password.RequireDigit = false;
    config.Password.RequiredLength = 6;
    config.Password.RequireLowercase = false;
    config.Password.RequireNonAlphanumeric = false;
    config.Password.RequireUppercase = false;
    config.SignIn.RequireConfirmedEmail = false;
}).AddEntityFrameworkStores<KvdemyDbContext>()
                   .AddDefaultTokenProviders().AddDefaultUI();
builder.Services.AddMemoryCache();


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
builder.Services.Configure<RequestLocalizationOptions>(
    opts =>
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

//use middlware exception handler
app.UseExceptionHandler(options => options.UseMiddleware<ExceptionHandler>());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");



app.MapRazorPages();

app.Run();
