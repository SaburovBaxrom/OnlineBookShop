namespace OnlineBookShopMvc.Models
{
	public class Order
	{
		public Guid Id { get; set; }
		public List<OrderItem> OrderItems { get; set; } = new();
		public decimal OrderTotal { get; set; }
		public DateTime OrderPlacedAt { get; set; }
	}
}
