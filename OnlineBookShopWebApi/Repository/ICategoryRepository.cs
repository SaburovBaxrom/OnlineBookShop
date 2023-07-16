using OnlineBookShopWebApi.Models.Dto;

namespace OnlineBookShopWebApi.Repository;

public interface ICategoryRepository
{
	Task<CategoryDto> CreatCategory(CreatationCategory category);
	Task<CategoryDto?> DeleteCategory(Guid id);
	Task<CategoryDto?> GetCategoryById(Guid id);
	Task<List<CategoryDto>> GetAllCategories();
}

