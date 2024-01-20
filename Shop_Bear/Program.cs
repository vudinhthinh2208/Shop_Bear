using Microsoft.EntityFrameworkCore;
using Shop_Bear.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ShopBearContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyCS"));
});
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(30);
	//options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});
var app = builder.Build();
app.UseSession();

app.UseStatusCodePagesWithRedirects("/Home/Error?statuscode={0}");


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
// Use session
app.UseAuthorization();

app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
