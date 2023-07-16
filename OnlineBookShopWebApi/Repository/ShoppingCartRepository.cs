using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OnlineBookShopWebApi.Data;
using OnlineBookShopWebApi.Models;
using OnlineBookShopWebApi.Models.Dto;

namespace OnlineBookShopWebApi.Repository
{
	public class ShoppingCartRepository : IShoppingCartRepository
	{
		private readonly BookShopDbContext _context;
		private readonly IMapper _mapper;
		public ShoppingCartRepository(BookShopDbContext context,IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<ShoppingCartDto> CreateShoppingCart(ShoppingCartCreatationDto shoppingCreatationDto)
		{
			var shoppingCart = _mapper.Map<ShoppingCart>(shoppingCreatationDto);
			
			await _context.ShoppingCarts.AddAsync(shoppingCart);
			await _context.SaveChangesAsync();

			var shoppingCartDto = _mapper.Map<ShoppingCartDto>(shoppingCart);

			return shoppingCartDto;
		}

		public async Task<List<ShoppingCartDto>> DeleteShoppingCart(Guid userId)
		{
			var shoppingCarts = await _context.ShoppingCarts.Where(cart => cart.UserId == userId).ToListAsync();

			foreach(var shoppingCart in shoppingCarts)
			{
				shoppingCart.isDeleted = true;
			}
			await _context.SaveChangesAsync();

			return _mapper.Map<List<ShoppingCartDto>>(shoppingCarts);
		}

		public async Task<ShoppingCartDto?> DeleteShoppingCartItem(Guid itemId)
		{
			var shoppingCartItem = await _context.ShoppingCarts.FirstOrDefaultAsync(x => x.Id == itemId);
			if (shoppingCartItem == null)
			{
				return null;
			}
			shoppingCartItem.isDeleted = true;
			await _context.SaveChangesAsync();
			return _mapper.Map<ShoppingCartDto>(shoppingCartItem);
			
		}

		public async Task<List<ShoppingCartDto>?> GetAllItems(Guid userId)
		{
			var shoppingCarts = await _context.ShoppingCarts.Where(cart =>  cart.UserId == userId && cart.isDeleted== false ).ToListAsync();

			if(shoppingCarts == null)
			{
				return null;
			}
			return _mapper.Map<List<ShoppingCartDto>>(shoppingCarts);


		}
	}
}
