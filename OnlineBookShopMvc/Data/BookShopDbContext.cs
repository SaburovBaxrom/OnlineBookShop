
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineBookShopMvc.Models;

namespace OnlineBookShopMvc.Data
{
    public class BookShopDbContext : IdentityDbContext<DefaultUser>
    {
        public BookShopDbContext (DbContextOptions<BookShopDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }

        public DbSet<Author>? Author { get; set; }

        public DbSet<Category>? Category { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }


	}
}
