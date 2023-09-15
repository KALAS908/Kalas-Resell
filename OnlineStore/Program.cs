using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OnlineStore.BusinessLogic.Base;
using OnlineStore.DataAccess;
using OnlineStore.DataAccess.EntityFramework.Context;
using OnlineStore.Entities;
using OnlineStore.WebApp;
using OnlineStore.WebApp.Code;
using OnlineStore.WebApp.Code.ExtensionsMethods;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddDbContext<StoreDataBaseContext>();
builder.Services.AddScoped<UnitOfWork>();
builder.Services.AddAutoMapper(options =>
     {
         options.AddMaps(typeof(Program), typeof(BaseService));
     });
builder.Services.AddPresentation();
builder.Services.AddCurentUser();
builder.Services.AddOnlineStoreBusinessLogic();
builder.Services.AddAuthentication("OnlineStoreCookies")
    .AddCookie("OnlineStoreCookies", options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(typeof(GlobalExceptionFilterAttribute));
});

builder.Services.AddRazorPages();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
