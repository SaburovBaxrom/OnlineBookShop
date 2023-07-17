using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineBookShopWebApi.Data;
using OnlineBookShopWebApi.Models;
using OnlineBookShopWebApi.Models.Dto;

namespace OnlineBookShopWebApi.Repository
{
	public class AuthorRepository : IAuthorRepository
	{
		private readonly IMapper _mapper;
		private readonly BookShopDbContext _dbContext;
		public AuthorRepository(IMapper mapper, BookShopDbContext dbContext)
		{
			_mapper = mapper;
			_dbContext = dbContext;
		}
		public async Task<AuthorDto> CreateAuhtorAsync(AuthorCreatationDto authorCreatationDto)
		{
			var author = _mapper.Map<Author>(authorCreatationDto);

			await _dbContext.Authors.AddAsync(author);
			await _dbContext.SaveChangesAsync();

			return _mapper.Map<AuthorDto>(author);
		}

		public async Task<List<AuthorDto>?> GetAllAuthorsAsync()
		{
			var authors = await _dbContext.Authors.ToListAsync();

			if (authors == null) 
				return null;

			return _mapper.Map<List<AuthorDto>>(authors);	
		}

		public async Task<AuthorDto?> GetAuthorAsync(Guid id)
		{
			var author = await _dbContext.Authors.FirstOrDefaultAsync(x => x.Id == id);

			if(author == null)
				return null;

			return _mapper.Map<AuthorDto>(author);
		}

		public async Task<List<BookDto>> GetAuthorsBooks(Guid id)
		{
			var books = await _dbContext.Books.Where(x => x.AuthorId == id).ToListAsync();

			return _mapper.Map<List<BookDto>>(books);
		}

		public async Task<AuthorDto?> UpdateAuhtorAsync(Guid id,AuthorUpdateDto authorUpdateDto)
		{
			var author = await _dbContext.Authors.FirstOrDefaultAsync(x => x.Id == id);

			if(author == null) 
				return null;
			
			author.FirstName = authorUpdateDto.FirstName;
			author.LastName = authorUpdateDto.LastName;
			
			await _dbContext.SaveChangesAsync();

			return _mapper.Map<AuthorDto>(author);
		}
	}
}
