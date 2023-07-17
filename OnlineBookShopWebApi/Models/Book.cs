namespace OnlineBookShopWebApi.Models;

public class Book
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public Guid AuthorId { get; set; }	
	public decimal Price { get; set; }
	public string Language { get; set; }
	public double Rating { get; set; }	
	public string ImageUrl { get; set; }
	public int Discount { get; set; }

	public Guid CategoryId { get; set; }
	public Category Category { get; set; }
	public Author Author { get; set; }
 
}
