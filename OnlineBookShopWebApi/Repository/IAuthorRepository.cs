using OnlineBookShopWebApi.Models.Dto;

namespace OnlineBookShopWebApi.Repository
{
	public interface IAuthorRepository
	{
		Task<AuthorDto> CreateAuhtorAsync(AuthorCreatationDto authorCreatationDto);
		Task<AuthorDto?> UpdateAuhtorAsync(Guid id, AuthorUpdateDto authorUpdateDto);
		Task<List<AuthorDto>?> GetAllAuthorsAsync();
		Task<AuthorDto?> GetAuthorAsync(Guid id);
		Task<List<BookDto>> GetAuthorsBooks(Guid id);

	}
}
