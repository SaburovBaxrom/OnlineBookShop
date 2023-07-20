using Microsoft.EntityFrameworkCore;
using OnlineBookShopMvc.Data;

namespace OnlineBookShopMvc.Models
{
    public class Cart
    {
        private readonly BookShopDbContext _context;
        public Cart(BookShopDbContext context)
        {
            _context = context;
        }

        public string Id { get; set; }
        public List<CartItem> CartItems { get; set; }
        public static Cart GetCart(IServiceProvider service)
        {
            ISession session = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var context = service.GetService<BookShopDbContext>();

            string cartId = session.GetString("Id")??Guid.NewGuid().ToString();
            session.SetString("Id", cartId);

            return new Cart(context) { Id = cartId };
        }

        public List<CartItem> GetAllCartItem()
        {
            return CartItems ??(_context.CartItems
                .Where(ci => ci.CardId == Id)
                .Include(ci => ci.Book)
                .ToList());
        }
        public decimal GetCartTotal()
        {
            return _context.CartItems
                .Where(ci => ci.CardId == Id)
                .Select(ci => ci.Quantity * (ci.Book.Price - (ci.Book.Discount * ci.Book.Price)/100))
                .Sum(); 
        }
        public CartItem GetCartItem(Book book)
        {
            return _context.CartItems.SingleOrDefault(ci => 
                ci.Book.Id == book.Id && ci.CardId == Id);
        }

        public void AddToCart(Book book, int quantity)
        {
            var cartItem = GetCartItem(book);
            if(cartItem == null) 
            {
                cartItem = new CartItem
                {
                    Id = Guid.NewGuid().ToString(),
                    Book = book,
                    Quantity = quantity,
                    CardId = Id
                };
                _context.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += quantity;
            }
            _context.SaveChanges();

        }

        public int ReduceQuantity(Book book)
        {
            var cartItem = GetCartItem(book);
            var remainingQuantity = 0;
            if(cartItem != null ) 
            { 
                if(cartItem.Quantity > 1)
                {
                    remainingQuantity = --cartItem.Quantity;
                }
                else
                {
                    _context.CartItems.Remove(cartItem);
                }
            }
            _context.SaveChanges();
            return remainingQuantity;
        }
		public int IncreaseQuantity(Book book)
		{
			var cartItem = GetCartItem(book);
			var remainingQuantity = 0;
			if (cartItem != null)
			{
				if (cartItem.Quantity > 0)
				{
					remainingQuantity = ++cartItem.Quantity;
				}
				
			}
			_context.SaveChanges();
			return remainingQuantity;
		}
	    public void RemoveFromCart(Book book)
        {
            var cartItem = GetCartItem(book);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
            }
            _context.SaveChanges();
        }

        public void ClearCart()
        {
            var cartItems = _context.CartItems.Where(ci => ci.CardId == Id);

            _context.CartItems.RemoveRange(cartItems);

            _context.SaveChanges();
        }
    }
}
