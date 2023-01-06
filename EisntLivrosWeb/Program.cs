using EisntLivros.DataAccess.Data;
using EisntLivros.DataAccess.Repository;
using EisntLivros.DataAccess.Repository.IRepository;
using EisntLivros.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Globalization and Localization Support
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.Configure<RequestLocalizationOptions>(options =>
    {
        List<CultureInfo> supportedCultures = new()
        {
            new CultureInfo("en-US"),
            new CultureInfo("pt-PT"),
            new CultureInfo("fr-FR"),
            new CultureInfo("es-ES")
        };

        options.DefaultRequestCulture = new RequestCulture(supportedCultures[0]);
        options.SupportedCultures = supportedCultures;
        options.SupportedUICultures = supportedCultures;
    });
builder.Services.AddMvc()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();

// DB Context
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("LocalDB")));

// Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(/*options => options.SignIn.RequireConfirmedAccount = true*/)
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddSingleton<IEmailSender, EmailSender>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
