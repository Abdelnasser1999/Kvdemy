using Kvdemy.Infrastructure.Services.Interfaces;
using Kvdemy.Core.Options;
using Kvdemy.Data.Models;
using Kvdemy.Infrastructure.Middlewares;
using Kvdemy.Web.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Globalization;
using System.Text;
using Kvdemy.Infrastructure.Services.PushNotification;
using Krooti.Infrastructure.Services.PushNotification;
using Kvdemy.Infrastructure.Services.Notifications;
using Krooti.Infrastructure.Services.Notifications;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using FirebaseAdmin.Messaging;
using Kvdemy.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<KvdemyDbContext>(options =>
	options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IInterfaceServices, InterfaceServices>();
builder.Services.AddScoped<IPushNotificationService, PushNotificationService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
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
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

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
builder.Services.AddControllersWithViews().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddMemoryCache();



builder.Services.AddAutoMapper(typeof(Kvdemy.infrastructure.Mapper.AutoMapper).Assembly);
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


/*.AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);*/
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddOptions<JwtOptions>(builder.Configuration["Jwt"]);

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issure"],
                ValidAudience = builder.Configuration["Jwt:Issure"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecurityKey"]))
            };
        });
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Kvdemy API", Version = "v1" });
    c.AddSecurityDefinition("Bearer",
        new OpenApiSecurityScheme
        {
            Description = "Please enter into field the word 'Bearer' following by space and JWT",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Scheme = "Bearer"
        });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
});
builder.Services.AddLocalization();

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
var app = builder.Build();
app.UseCors("AllowAll"); // تطبيق سياسة CORS

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Kvdemy API");
    });
}
else if (app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/Kvdemy/swagger/v1/swagger.json", "Kvdemy API");
    });
}


//use middlware exception handler
app.UseExceptionHandler(options => options.UseMiddleware<ExceptionHandler>());

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseRequestLocalization();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=swagger}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
