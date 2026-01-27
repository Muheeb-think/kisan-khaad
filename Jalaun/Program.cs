using BAL;
using BAL.Services;
using DAL.MasterData;
using DAL.Repo;
using DAL.SqlHeplers;
using Jalaun;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseWebRoot("wwwroot");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IDBHelpers, DBHelper>();
builder.Services.AddScoped<IDataAccess, DataAccess>();
builder.Services.AddScoped<IMastersData, MasterData>();
builder.Services.AddScoped<IRegistration, Registration>();
builder.Services.AddScoped<IMasterDataBAL, MasterDataBAL>();
builder.Services.AddScoped<ICommonLogics, CommonLogics>();
builder.Services.AddScoped<IFarmerDataDAL, FarmerDataDAL>();
builder.Services.AddScoped<IFarmerDataBAL, FarmerDataBal>();



builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;

}).AddCookie(option =>
{
    option.AccessDeniedPath = "/Home/Accessdenied";
    option.LoginPath = "/Auth/login";
    option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    option.SlidingExpiration = true; // refreshes expiry on activity
});

builder.Services.AddScoped<LoginUserMiddleware>();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

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

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
app.CustomeMiddleSetSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
