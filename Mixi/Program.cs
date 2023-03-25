using Microsoft.CodeAnalysis.Options;
using Mixi.IServices;
using Mixi.Services;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IProductServices, ProductServices>();//???c kh?i t?o m?i khi c� y�u c?u, m?i request s? ???c nh?n 1 service kh�c nhau v� ?c s? d?ng v?i c�c service c� nhi?u y�u c?u http
builder.Services.AddTransient<IColorServices, ColorServices>();
builder.Services.AddTransient<ICategoryServices, CategoryServices>();
builder.Services.AddTransient<ISizeServices, SizeServices>();
builder.Services.AddTransient<IImageServices, ImageServices>();
//builder.Services.AddSingleton<IProductServices, ProductServices>();//service ch? ?c t?o 1 l?n trong su?t lifetime,ph� h?p cho c�c service c� t�nh to�n c?c v� k thay ??i
//builder.Services.AddScoped<IProductServices, ProductServices>();//l� m?i l?n request l� s? t?o l?i service 1 l?n,d�ng cho c�c service c� t�nh ch?t ??c th� n�o ?�
//khai bao session voi thoi gian timeout laf 30
builder.Services.AddSession(Options =>
{
    Options.IdleTimeout = TimeSpan.FromSeconds(5);
});
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
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
