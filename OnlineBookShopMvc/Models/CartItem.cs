namespace OnlineBookShopMvc.Models
{
    public class CartItem
    {
        public string Id { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }
        public string CardId { get; set; }    
    }
}
