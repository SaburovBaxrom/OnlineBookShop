using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineBookShopWebApi.Models.Dto;
using OnlineBookShopWebApi.Repository;

namespace OnlineBookShopWebApi.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class BookController : ControllerBase
	{
		private readonly IBookRepository bookRepository;
		public BookController(IBookRepository bookRepository) 
		{ 
			this.bookRepository = bookRepository;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllAsync([FromQuery] FilterSortPaginationDto filterSortPaginationDto )
		{
			var books = await bookRepository.GetAllBooksAsync(filterSortPaginationDto);
			return Ok(books);	
		}

		[HttpPost]
		public async Task<IActionResult> CreateAsync([FromBody] BookCreatationDto book)
		{
			var responseBook = await bookRepository.CreateBookAsync(book);

			return Ok(responseBook);
		}

		[HttpGet]
		[Route("{id:Guid}")]
		public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
		{
			var response = await bookRepository.GetBookByIdAsync(id);
			if(response == null)
			{
				return NotFound();
			}
			return Ok(response);
		}

		[HttpDelete]
		[Route("{id:Guid}")]
		public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
		{
			var deletedBook = await bookRepository.DeleteBookAsync(id);
			if(deletedBook != null) 
			{
				return NotFound();
			}
			return Ok(deletedBook);
		}

		[HttpPut]
		[Route("{id:Guid}")]
		public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateBookDto book)
		{
			var updatedBook = await bookRepository.UpdateBookAsync(id, book);
			if(book == null)
			{
				return NotFound();
			}
			return Ok(updatedBook);
		}

		[HttpPost]
		public async Task<IActionResult> RateBook([FromQuery] Guid userId, [FromQuery] Guid bookId, short rate)
		{
			await bookRepository.RatingBookAsync(userId,bookId,rate);
			return Ok(rate);
		}
		[HttpGet]
		[Route("{id:Guid}")]
		public async Task<IActionResult> GetRatingBook([FromRoute]Guid id)
		{
			var rating = await bookRepository.BookRating(id);

			return Ok(rating);
		}
	}
}
