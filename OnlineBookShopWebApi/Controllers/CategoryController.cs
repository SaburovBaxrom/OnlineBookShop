using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineBookShopWebApi.Models.Dto;
using OnlineBookShopWebApi.Repository;

namespace OnlineBookShopWebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly ICategoryRepository _categoryRepository;

		public CategoryController(ICategoryRepository categoryRepository)
		{
			_categoryRepository = categoryRepository;
		}

		[HttpPost]
		public async Task<IActionResult> CreateAsync(CreatationCategory creatationCategory)
		{
			var catergory = await _categoryRepository.CreatCategory(creatationCategory);

			if(catergory== null)
			{
				return BadRequest();
			}
			return Ok(catergory);
		}

		[HttpGet]
		public async Task<IActionResult> GetAllAsync()
		{
			var categories = await _categoryRepository.GetAllCategories();

			if(categories== null) 
			{ 
				return NotFound();
			}

			return Ok(categories);

		}
		[HttpDelete]
		[Route("{id:Guid}")]
		public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
		{
			var deletedCatergory = await _categoryRepository.DeleteCategory(id);

			if(deletedCatergory == null)
			{
				return NotFound();
			}

			return Ok(deletedCatergory);
		}
		[HttpGet]
		[Route("{id:Guid}")]
		public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
		{
			var category = await _categoryRepository.GetCategoryById(id);

			if(category == null)
			{
				return NotFound();
			}

			return Ok(category);
		}
	}
}
