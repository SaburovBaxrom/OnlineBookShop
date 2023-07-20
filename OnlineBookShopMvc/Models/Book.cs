using System.ComponentModel.DataAnnotations;

namespace OnlineBookShopMvc.Models
{
	public class Book
	{

		public Guid Id { get; set; }
		[Required]
		[MaxLength(100)]
		public string Name { get; set; }
		[Required]
		public Guid AuthorId { get; set; }
		[Required]
		public decimal Price { get; set; }
		public string Language { get; set; }
		public double? Rating { get; set; } = 0;
		public string ImageUrl { get; set; }
		public int Discount { get; set; }

		public Guid CategoryId { get; set; }
		public Category? Category { get; set; }
		public Author? Author { get; set; }
	}
}
