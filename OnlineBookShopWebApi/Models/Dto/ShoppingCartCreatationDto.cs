namespace OnlineBookShopWebApi.Models.Dto
{
	public class ShoppingCartCreatationDto
	{
		public Guid BookId { get; set; }
		public Guid UserId { get; set; }
	}
}
