using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineBookShopWebApi.Models.Dto;
using OnlineBookShopWebApi.Repository;

namespace OnlineBookShopWebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ShoppingCartController : ControllerBase
	{
		private readonly IShoppingCartRepository _shoppingCartRepository;
		public ShoppingCartController(IShoppingCartRepository shoppingCart)
		{
			_shoppingCartRepository= shoppingCart;
		}
		[HttpPost]
		public async Task<IActionResult> CreateCartItem([FromBody] ShoppingCartCreatationDto shoppingCart)
		{
			var item = await _shoppingCartRepository.CreateShoppingCart(shoppingCart);

			return Ok(item);
		}

		[HttpGet]
		[Route("{id}")]
		public async Task<IActionResult> GetAllItems(Guid id) 
		{
			var items = await _shoppingCartRepository.GetAllItems(id);
			if(items == null)
			{
				return NotFound();
			}
			return Ok(items);
		}

		[HttpDelete]
		[Route("{userId:Guid}")]
		public async Task<IActionResult> DeleteShoppingCart([FromRoute] Guid userId)
		{
			var items = await _shoppingCartRepository.DeleteShoppingCart(userId);

			if (items == null)
			{
				return NotFound();
			}

			return Ok(items);
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteShoppingCartItem([FromQuery] Guid itemId)
		{
			var item = await _shoppingCartRepository.DeleteShoppingCartItem(itemId);
			if (item == null)
			{
				return NotFound();
			}

			return Ok(item);
		}
	}
}
