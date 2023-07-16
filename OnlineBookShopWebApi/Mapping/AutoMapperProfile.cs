using AutoMapper;
using OnlineBookShopWebApi.Models;
using OnlineBookShopWebApi.Models.Dto;

namespace OnlineBookShopWebApi.Mapping
{
	public class AutoMapperProfile : Profile
	{

		public AutoMapperProfile() 
		{
			CreateMap<UpdateBookDto, Book>().ReverseMap();
			CreateMap<BookCreatationDto, Book>().ReverseMap();
			CreateMap<Book, BookDto>().ReverseMap();
			CreateMap<CreatationCategory, Category>().ReverseMap();
			CreateMap<Category, CategoryDto>().ReverseMap();
			CreateMap<ShoppingCartCreatationDto, ShoppingCart>().ReverseMap();	
			CreateMap<ShoppingCart, ShoppingCartDto>().ReverseMap();
		}
	}
}
