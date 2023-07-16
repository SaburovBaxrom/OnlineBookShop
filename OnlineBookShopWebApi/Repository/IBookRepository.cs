using OnlineBookShopWebApi.Models;
using OnlineBookShopWebApi.Models.Dto;

namespace OnlineBookShopWebApi.Repository
{
	public interface IBookRepository
	{
		Task<BookDto> CreateBookAsync(BookCreatationDto creatationDto);
		Task<BookDto?> DeleteBookAsync(Guid id);
		Task<List<BookDto>> GetAllBooksAsync(FilterSortPaginationDto filterSortPaginationDto);
		Task<BookDto?> GetBookByIdAsync(Guid id);
		Task<BookDto> UpdateBookAsync(Guid id, UpdateBookDto updateBookDto);
		Task<BookRating> RatingBookAsync(Guid userId, Guid bookId,short rating);
		Task<double> BookRating(Guid bookId);


	}
}
