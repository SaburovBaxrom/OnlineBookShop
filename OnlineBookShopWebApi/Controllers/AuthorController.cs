using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using OnlineBookShopWebApi.Models.Dto;
using OnlineBookShopWebApi.Repository;

namespace OnlineBookShopWebApi.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	[Authorize]
	public class AuthorController : ControllerBase
	{
		private readonly IAuthorRepository auhtorRepository;
		public AuthorController(IAuthorRepository auhtorRepository)
		{
			this.auhtorRepository = auhtorRepository;
		}

		[HttpPost]
		public async Task<IActionResult> CreateAuthor(AuthorCreatationDto authorCreatation)
		{
			var author = await auhtorRepository.CreateAuhtorAsync(authorCreatation);

			if(author == null)
			{
				return BadRequest("!");
			}
			return Ok(author);
		}

		[HttpGet]
		public async Task<IActionResult> GetAllAuthors()
		{
			var authors = await auhtorRepository.GetAllAuthorsAsync();

			if (authors == null)
				return NotFound();

			return Ok(authors);
		}
		[HttpGet]
		[Route("{id:Guid}")]
		public async Task<IActionResult> GetAuthorById([FromRoute] Guid id)
		{
			var author = await auhtorRepository.GetAuthorAsync(id);

			if(author == null)
			{
				return NotFound();
			}

			return Ok(author);
		}

		[HttpGet]
		public async Task<IActionResult> GetAuthorsBooks([FromQuery]Guid id)
		{
			var book = await auhtorRepository.GetAuthorsBooks(id);
			if (book == null)
				return NotFound();

			return Ok(book);
		}

		[HttpPut]
		[Route("{id:Guid}")]
		public async Task<IActionResult> UpdateAuthor([FromRoute] Guid id,AuthorUpdateDto authorUpdateDto )
		{
			var author = await auhtorRepository.UpdateAuhtorAsync(id, authorUpdateDto);

			if (author == null)
				return NotFound();

			return Ok(author);
		}
	}
}
