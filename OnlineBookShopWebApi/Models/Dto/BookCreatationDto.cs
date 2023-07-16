namespace OnlineBookShopWebApi.Models.Dto
{
	public class BookCreatationDto
	{
		public string Name { get; set; }
		public decimal Price { get; set; }
		public string Language { get; set; }
		public string ImageUrl { get; set; }
		public Guid CategoryId { get; set; }
		public int Discount { get; set; }
	}
}
