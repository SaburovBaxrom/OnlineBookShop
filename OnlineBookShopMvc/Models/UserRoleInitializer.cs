using Microsoft.AspNetCore.Identity;

namespace OnlineBookShopMvc.Models
{
	public static class UserRoleInitializer
	{
		public static async Task InitializeAsync(IServiceProvider serviceProvider)
		{
			var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
			var userManager = serviceProvider.GetRequiredService<UserManager<DefaultUser>>();
			string[] roleNames = { "Admin", "User" };

			IdentityResult roleResult;

			foreach (var role in roleNames)
			{
				var roleExsist = await roleManager.RoleExistsAsync(role);

				if (!roleExsist)
				{
					roleResult = await roleManager.CreateAsync(new IdentityRole(role));
				}
			}

			var email = "admin@site.com";
			var password = "Qwerty123!";

			if(userManager.FindByEmailAsync(email).Result == null)
			{
				DefaultUser user = new()
				{
					Email = email,
					UserName = email,
					FirstName = "Admin",
					LastName = "Big Admin",
					Address = "Anywhere",
					City = "Big city"
					
				};
				IdentityResult result = userManager.CreateAsync(user, password).Result;
				userManager.ConfirmEmailAsync(user,"token").Wait();
				
				if(result.Succeeded)
				{
					userManager.AddToRoleAsync(user, "Admin").Wait();
				}
			}
		}
	}
}
