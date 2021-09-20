using System.Collections.Generic;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.InputModels;

namespace TechnicalRadiation.Services.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDto> GetAllCategories();
        CategoryDetailDto GetCategoryById(int id);
        bool LinkCategoryByCategoryIdAndNewsItemId(int categoryId, int newsItemId);
        int CreateCategory(CategoryInputModel category);
        void UpdateCategory(int id, CategoryInputModel categoryData);
        void DeleteCategory(int id);
    }
}