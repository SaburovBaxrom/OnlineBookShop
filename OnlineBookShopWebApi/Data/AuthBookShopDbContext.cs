using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace OnlineBookShopWebApi.Data
{
	public class AuthBookShopDbContext : IdentityDbContext
	{
		public AuthBookShopDbContext(DbContextOptions<AuthBookShopDbContext> options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			var adminRoleId = "36c2e8d5-c6a9-4447-81ff-cbcc91813cd7";
			var userRoleId = "e9716b6a-25b6-4edc-8cf3-7c1b099cce01";

			var roles = new List<IdentityRole>
			{
				new IdentityRole
				{
					Id = adminRoleId,
					ConcurrencyStamp = adminRoleId,
					Name = "admin",
					NormalizedName= "admin".ToUpper()
				},
				new IdentityRole
				{
					Id = userRoleId,
					ConcurrencyStamp = userRoleId,
					Name = "user",
					NormalizedName= "user".ToUpper()
				}
			};

			builder.Entity<IdentityRole>().HasData(roles);
		}
	}
}
