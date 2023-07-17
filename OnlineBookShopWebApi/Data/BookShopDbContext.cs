using Microsoft.EntityFrameworkCore;
using OnlineBookShopWebApi.Models;

namespace OnlineBookShopWebApi.Data;

public class BookShopDbContext : DbContext
{
	public BookShopDbContext(DbContextOptions<BookShopDbContext> options) 
		: base(options)
	{

	}

	public DbSet<Book> Books { get; set; }
	public DbSet<BookRating> Rating { get; set; }
	public DbSet<Category> Categories { get; set; }
	public DbSet<ShoppingCart> ShoppingCarts { get; set;}
	public DbSet<Author> Authors { get; set; }

}
