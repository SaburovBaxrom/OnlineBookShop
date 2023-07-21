using Microsoft.EntityFrameworkCore;
using OnlineBookShopMvc.Data;
using OnlineBookShopMvc.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BookShopDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BookShopDbContext") ?? throw new InvalidOperationException("Connection string 'BookShopDbContext' not found.")));

builder.Services.AddDefaultIdentity<DefaultUser>(options => options.SignIn.RequireConfirmedAccount = true)
	.AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<BookShopDbContext>();

builder.Services.AddRazorPages();
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
using(var scope = app.Services.CreateScope())
{
	var service = scope.ServiceProvider;
	try
	{
		UserRoleInitializer.InitializeAsync(service).Wait();
	}
	catch(Exception ex) 
	{
		var logger = service.GetRequiredService<ILogger<Program>>();
		logger.LogError(ex, "An error occured while attempting to seed the database");
	}
}
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

app.UseAuthentication();;
app.UseAuthorization();

app.UseSession();

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllerRoute(
		name: "default",
		pattern: "{controller=Store}/{action=Index}/{id?}");
	endpoints.MapRazorPages();
});

app.Run();
