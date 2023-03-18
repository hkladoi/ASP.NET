using Mixi.IServices;
using Mixi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IProductServices, ProductServices>();//???c kh?i t?o m?i khi có yêu c?u, m?i request s? ???c nh?n 1 service khác nhau và ?c s? d?ng v?i các service có nhi?u yêu c?u http
builder.Services.AddSingleton<IColorServices, ColorServices>();
builder.Services.AddSingleton<ICategoryServices, CategoryServices>();
builder.Services.AddSingleton<ISizeServices, SizeServices>();
//builder.Services.AddSingleton<IProductServices, ProductServices>();//service ch? ?c t?o 1 l?n trong su?t lifetime,phù h?p cho các service có tính toàn c?c và k thay ??i
//builder.Services.AddScoped<IProductServices, ProductServices>();//là m?i l?n request là s? t?o l?i service 1 l?n,dùng cho các service có tính ch?t ??c thù nào ?ó
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
