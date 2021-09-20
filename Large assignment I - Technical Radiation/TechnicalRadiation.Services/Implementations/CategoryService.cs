using System;
using System.Collections.Generic;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.Extensions;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Repositories.Interfaces;
using TechnicalRadiation.Services.Interfaces;

namespace TechnicalRadiation.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IEnumerable<CategoryDto> GetAllCategories()
        {
            var categories = _categoryRepository.GetAllCategories();

            return categories;
        }

        public CategoryDetailDto GetCategoryById(int id)
        {
            var category = _categoryRepository.GetCategoryById(id);

            return category;
        }

        public bool LinkCategoryByCategoryIdAndNewsItemId(int categoryId, int newsItemId)
        {
            return _categoryRepository.LinkCategoryByCategoryIdAndNewsItemId(categoryId, newsItemId);
        }

        public int CreateCategory(CategoryInputModel category)
        {
            return _categoryRepository.CreateCategory(category);
        }

        public void UpdateCategory(int id, CategoryInputModel categoryData)
        {
            _categoryRepository.UpdateCategory(id, categoryData);
        }

        public void DeleteCategory(int id) 
        {
            _categoryRepository.DeleteCategory(id);
        }
        
    }
}