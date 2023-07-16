namespace OnlineBookShopWebApi.Models;

public class ShoppingCart
{
	public Guid Id { get; set; }
	public Guid BookId { get; set; }
	public Guid UserId { get; set; }
	public bool isDeleted { get; set; } = false;
	public bool isBought { get; set; } = false;
}