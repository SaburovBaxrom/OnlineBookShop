using OnlineBookShopWebApi.Models.Dto;

namespace OnlineBookShopWebApi.Repository
{
	public interface IShoppingCartRepository
	{
		Task<ShoppingCartDto> CreateShoppingCart(ShoppingCartCreatationDto shoppingCreatationDto);
		Task<ShoppingCartDto?> DeleteShoppingCartItem(Guid itemId);
		Task<List<ShoppingCartDto>> DeleteShoppingCart(Guid userId);
		Task<List<ShoppingCartDto>?> GetAllItems(Guid userId);
	}
}
