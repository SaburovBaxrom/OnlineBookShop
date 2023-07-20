using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineBookShopMvc.Models;

namespace OnlineBookShopMvc.Data
{
    public class BookShopDbContext : DbContext
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
