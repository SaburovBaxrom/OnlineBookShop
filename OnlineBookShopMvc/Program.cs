using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineBookShopMvc.Data;
using OnlineBookShopMvc.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BookShopDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BookShopDbContext") ?? throw new InvalidOperationException("Connection string 'BookShopDbContext' not found.")));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<Cart>(sp => Cart.GetCart(sp));
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
	options.Cookie.HttpOnly= true;
	options.Cookie.IsEssential= true;
	//options.IdleTimeout = TimeSpan.FromSeconds(10);
});
// Add services to the container.
builder.Services.AddControllersWithViews();

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
app.UseSession();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Store}/{action=Index}/{id?}");

app.Run();
