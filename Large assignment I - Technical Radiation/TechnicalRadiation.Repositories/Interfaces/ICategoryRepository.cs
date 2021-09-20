using System.Collections.Generic;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.InputModels;

namespace TechnicalRadiation.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<CategoryDto> GetAllCategories();
        CategoryDetailDto GetCategoryById(int id);
        bool LinkCategoryByCategoryIdAndNewsItemId(int categoryId, int newsItemId);
        int CreateCategory(CategoryInputModel category);
        void UpdateCategory(int id, CategoryInputModel categoryData);
        void DeleteCategory(int id);
    }
}