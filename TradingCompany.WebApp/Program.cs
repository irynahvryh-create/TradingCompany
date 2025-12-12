using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TradingCompany.BL.Concrete;
using TradingCompany.BL.Interfaces;
using TradingCompany.DAL.EF.Concrete;
using TradingCompany.DAL.EF.MapperProfiles;
using TradingCompany.DAL.Interfaces;

var builder = WebApplication.CreateBuilder(args);





// Створюємо папку Logs, якщо її немає
var logFolder = Path.Combine(AppContext.BaseDirectory, "Logs");
if (!Directory.Exists(logFolder))
{
    Directory.CreateDirectory(logFolder);
}

// -----------------------------
// LOGGING
// -----------------------------
builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.ClearProviders();
    loggingBuilder.SetMinimumLevel(LogLevel.Debug);
    loggingBuilder.AddLog4Net("log4net.xml");
});

// -----------------------------
// AutoMapper
// -----------------------------
builder.Services.AddSingleton<IMapper>(sp =>
{
    var config = new MapperConfiguration(cfg =>
    {
        cfg.AddProfile<Category_Map>();
        cfg.AddProfile<User_Map>();
        cfg.AddProfile<Privilege_Map>();
    });

    return config.CreateMapper();
});


// -----------------------------
// CONNECTION STRING
// -----------------------------
string connStr = builder.Configuration.GetConnectionString("TradingCompanyDB") ?? "";

// -----------------------------
// DEPENDENCY INJECTION - DAL
// -----------------------------
builder.Services.AddTransient<ICategoryDal>(sp => new CategoryDalEF(connStr, sp.GetRequiredService<IMapper>()));
builder.Services.AddTransient<IUserDal>(sp => new UserDalEF(connStr, sp.GetRequiredService<IMapper>()));
builder.Services.AddTransient<IUserPrivilegeDal>(sp => new UserPrivilegeDalEF(connStr, sp.GetRequiredService<IMapper>()));

// -----------------------------
// DEPENDENCY INJECTION - BL
// -----------------------------
builder.Services.AddTransient<ICategoryManager, CategoryManager>();
builder.Services.AddTransient<IAuthManager, AuthManager>();

// -----------------------------
// MVC
// -----------------------------
builder.Services.AddControllersWithViews();

// -----------------------------
// Authentication (Cookie)
// -----------------------------
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // шлях до сторінки логіну
        options.AccessDeniedPath = "/Account/AccessDenied"; // для неадмінів
    });

// -----------------------------
// BUILD APP
// -----------------------------
var app = builder.Build();

// -----------------------------
// PIPELINE
// -----------------------------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

// Default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
