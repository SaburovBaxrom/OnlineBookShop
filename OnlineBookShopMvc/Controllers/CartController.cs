using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineBookShopMvc.Data;
using OnlineBookShopMvc.Models;
using System.Reflection.Metadata;

namespace OnlineBookShopMvc.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly Cart _cart;
        private readonly BookShopDbContext _context;
        public CartController(Cart cart, BookShopDbContext context)
        {
            _cart = cart;
            _context = context;
        }

        public IActionResult Index()
        {
            var items = _cart.GetAllCartItem();
            _cart.CartItems = items;
            return View(_cart);
        }
        public IActionResult AddToCart(Guid id)
        {
            var selectedBook = GetBookById(id);

            if(selectedBook != null)
            {
                _cart.AddToCart(selectedBook,quantity: 1);
            }

            return RedirectToAction("Index", "Store");
        }

        public Book GetBookById(Guid id)
        {
            return _context.Books.FirstOrDefault(x => x.Id == id);
        }

        public IActionResult RemoveFromCart(Guid id)
        {
            var selectedBook = GetBookById(id);

            if(selectedBook != null)
            {
                _cart.RemoveFromCart(selectedBook);
            }
            return RedirectToAction("Index");
        }

        public IActionResult ReduceQuantity(Guid id) 
        {
            var selectedBook = GetBookById(id);
            if(selectedBook != null)
            {
                _cart.ReduceQuantity(selectedBook);
            }
            return RedirectToAction("Index");
        }
		public IActionResult IncreaseQuantity(Guid id)
		{
			var selectedBook = GetBookById(id);
			if (selectedBook != null)
			{
				_cart.IncreaseQuantity(selectedBook);
			}
			return RedirectToAction("Index");
		}
        public IActionResult ClearCart()
        {
            _cart.ClearCart();
            return RedirectToAction("Index");
        }
	}
}
