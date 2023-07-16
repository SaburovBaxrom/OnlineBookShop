namespace OnlineBookShopWebApi.Models;

public class BookRating
{
	public Guid Id { get; set; }
	public Guid UserId { get; set; }	
	public Guid BookId { get; set; }
	public short Rate { get; set; }

}
