using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineBookShopWebApi.Data;
using OnlineBookShopWebApi.Models;
using OnlineBookShopWebApi.Models.Dto;

namespace OnlineBookShopWebApi.Repository
{
	public class CategoryRepository : ICategoryRepository
	{
		private readonly BookShopDbContext _dbContext;
		private readonly IMapper _mapper;
		public CategoryRepository(BookShopDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task<CategoryDto> CreatCategory(CreatationCategory creatationCategory)
		{
			var category = _mapper.Map<Category>(creatationCategory);

			await _dbContext.Categories.AddAsync(category);
			await _dbContext.SaveChangesAsync();

			var categoryDto = _mapper.Map<CategoryDto>(category);
			return categoryDto;
		}

		public async Task<CategoryDto?> DeleteCategory(Guid id)
		{
			var category = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
			if (category == null)
			{
				return null;
			}
			_dbContext.Categories.Remove(category);
			await _dbContext.SaveChangesAsync();
			return _mapper.Map<CategoryDto>(category);
		}

		public async Task<List<CategoryDto>> GetAllCategories()
		{
			var categories = await _dbContext.Categories.ToListAsync();
			return _mapper.Map<List<CategoryDto>>(categories);
		}

		public async Task<CategoryDto?> GetCategoryById(Guid id)
		{
			var category = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
			if (category == null)
			{
				return null;
			}
			return _mapper.Map<CategoryDto>(category);
		}
	}
}
