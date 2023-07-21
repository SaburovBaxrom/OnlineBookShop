using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineBookShopMvc.Data;
using OnlineBookShopMvc.Models;

namespace OnlineBookShopMvc.Controllers;

[Authorize]
public class OrderController : Controller
{
	private readonly BookShopDbContext _context;
	private readonly Cart _cart;

	public OrderController(BookShopDbContext context, Cart cart)
	{
		_context = context;
		_cart = cart;
	}

	public IActionResult Checkout()
	{
		return View();
	}

	[HttpPost]
	public IActionResult Checkout(Order order)
	{
		var cartItems = _cart.GetAllCartItem();
		_cart.CartItems = cartItems;

		if(_cart.CartItems.Count == 0) 
		{
			ModelState.AddModelError("", "cart is empty please add a book first");
		}

		if (ModelState.IsValid)
		{
			CreateOrder(order);
			_cart.ClearCart();
			return View("CheckoutComplete", order);	
		}
		return View(order);
	}

	public IActionResult CheckoutComplete(Order order)
	{
		return View(order);
	}
	public void CreateOrder(Order order)
	{
		order.OrderPlacedAt= DateTime.Now;

		var cartItems = _cart.CartItems;

		foreach (var item in cartItems)
		{
			var orderItem = new OrderItem()
			{
				Quantity = item.Quantity,
				BookId = item.Book.Id,
				OrderId = order.Id,
				Price = (item.Book.Price - (item.Book.Price * item.Book.Discount / 100)) * item.Quantity
			};
			order.OrderItems.Add(orderItem);
			order.OrderTotal += orderItem.Price;
		}
		_context.Orders.Add(order);
		_context.SaveChanges();

	}
}




