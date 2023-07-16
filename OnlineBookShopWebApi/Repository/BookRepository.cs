using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineBookShopWebApi.Data;
using OnlineBookShopWebApi.Models;
using OnlineBookShopWebApi.Models.Dto;
using System.Reflection.Emit;

namespace OnlineBookShopWebApi.Repository
{
	public class BookRepository : IBookRepository
	{
		private readonly BookShopDbContext _context;
		private readonly IMapper _mapper;
		private readonly UserManager<IdentityUser> userManager;
		public BookRepository(BookShopDbContext context, IMapper mapper,UserManager<IdentityUser> userManager)
		{
			_context = context;
			_mapper = mapper;
			this.userManager = userManager;
		}

		public async Task<double> BookRating(Guid bookId)
		{
			var rating = await _context.Rating.ToListAsync();

			var books = rating.Where(x => x.BookId == bookId).ToList();

			var ratedBook = await _context.Books.FirstOrDefaultAsync(x => x.Id == bookId);

			

			int sum = 0;
			foreach (var book in books)
			{
				sum += book.Rate;
			}
			
			if(books.Count == 0) { return 0; }
			if (ratedBook != null)
			{
				ratedBook.Rating = (double)sum / books.Count;
				await _context.SaveChangesAsync();
			}
			return (double)sum/books.Count;
		}

		public async Task<BookDto> CreateBookAsync(BookCreatationDto creatationDto)
		{
			var book = _mapper.Map<Book>(creatationDto);

			await _context.Books.AddAsync(book);
			await _context.SaveChangesAsync();

			var bookDto = _mapper.Map<BookDto>(book);

			return bookDto;
		}

		public async Task<BookDto?> DeleteBookAsync(Guid id)
		{
			var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);

			if(book == null)
			{
				return null;
			}

			_context.Books.Remove(book);
			await _context.SaveChangesAsync();
			var bookDto = _mapper.Map<BookDto>(book);
			return bookDto;
		}

		public async Task<List<BookDto>> GetAllBooksAsync(FilterSortPaginationDto dto)
		{

			var books = _context.Books.AsQueryable();
			//filtering

			if(string.IsNullOrWhiteSpace(dto.FilterOn) == false && string.IsNullOrWhiteSpace(dto.FilterQuery) == false)
			{
				if (dto.FilterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
				{
					books = books.Where(x => x.Name.Contains(dto.FilterQuery));
				}
				else if (dto.FilterOn.Equals("language",StringComparison.OrdinalIgnoreCase))
				{
					books = books.Where(x => x.Language == dto.FilterQuery);
				}
				else if (dto.FilterOn.Equals("price", StringComparison.OrdinalIgnoreCase))
				{
					books = books.Where(x => x.Price == int.Parse(dto.FilterQuery));
				}
				else if (dto.FilterOn.Equals("discount", StringComparison.OrdinalIgnoreCase))
				{
					books = books.Where(x => x.Discount > int.Parse(dto.FilterQuery));
				}
				else if (dto.FilterOn.Equals("category", StringComparison.OrdinalIgnoreCase))
				{
					books = books.Where(x => x.CategoryId.ToString() == dto.FilterQuery);
				}

			}
			//sorting
			if(string.IsNullOrWhiteSpace(dto.SortBy) == false)
			{
				if (dto.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
				{
					books = dto.SortOrder ? books.OrderBy(x => x.Name): books.OrderByDescending(x => x.Name);
				}
				if(dto.SortBy.Equals("Price", StringComparison.OrdinalIgnoreCase))
				{
					books = dto.SortOrder ? books.OrderBy(x => x.Price) :books.OrderByDescending(x => x.Price);
				}
				if (dto.SortBy.Equals("discount", StringComparison.OrdinalIgnoreCase))
				{
					books = dto.SortOrder ? books.OrderBy(x => x.Discount) : books.OrderByDescending(x => x.Discount);
				}
			}
			//pagination
			var skipResults = (dto.PageNumber - 1) * dto.PageSize;
			books = books.Skip(skipResults).Take(dto.PageSize);
			return _mapper.Map<List<BookDto>>(books);
		}

		public async Task<BookDto?> GetBookByIdAsync(Guid id)
		{
			var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);

			if(book == null)
			{
				return null;
			}

			var bookDto = _mapper.Map<BookDto>(book);
			return bookDto;
		}
		//Rating book
		public async Task<BookRating> RatingBookAsync(Guid userId, Guid bookId, short rating)
		{
			var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == bookId);

			var user = await userManager.FindByIdAsync(userId.ToString());

			var bookRating = new BookRating
			{
				UserId = userId,
				BookId = bookId,
				Rate = rating
			};

			if(user != null && book != null)
			{
				await _context.Rating.AddAsync(bookRating);
				await _context.SaveChangesAsync();
				return bookRating;
			}
			return null;
		}

		public async Task<BookDto> UpdateBookAsync(Guid id,UpdateBookDto updateBookDto)
		{
			var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);

			if(book == null)
			{
				return null;
			}

			book.Price = updateBookDto.Price;
			book.Name = updateBookDto.Name;
			book.Discount = updateBookDto.Discount;
			book.ImageUrl = updateBookDto.ImageUrl;
			book.Language = updateBookDto.Language;

			await _context.SaveChangesAsync();

			return _mapper.Map<BookDto>(book);
		}
	}
}
