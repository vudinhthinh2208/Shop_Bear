using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shop_Bear.Models;
using Shop_Bear.Models.EF;

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
//Đăng ký Identity
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
	options.Password.RequiredUniqueChars = 0;
	options.Password.RequireUppercase = false;
	options.Password.RequiredLength = 8;
	options.Password.RequireLowercase = false;
})
	.AddEntityFrameworkStores<ShopBearContext>()
	.AddDefaultTokenProviders();






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
app.UseAuthentication(); // xác thực trước
app.UseAuthorization(); // xác thực coi có quyền gì

app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
