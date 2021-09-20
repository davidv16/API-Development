using System;
using System.Collections.Generic;
using System.Linq;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.Entities;
using TechnicalRadiation.Models.Extensions;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Repositories.Contexts;
using TechnicalRadiation.Repositories.Interfaces;

namespace TechnicalRadiation.Repositories.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly NewsDbContext _dbContext;

        public CategoryRepository(NewsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<CategoryDto> GetAllCategories()
        {
            var categories = _dbContext.Categories.Select(n => new CategoryDto
            {
                Id = n.Id,
                Name = n.Name,
                Slug = n.Slug
            }).ToList();

            foreach (var category in categories)
            {
                category.Links.AddReference("self", new { href = $"api/categories/{category.Id}" });
                category.Links.AddReference("edit", new { href = $"api/categories/{category.Id}" });
                category.Links.AddReference("delete", new { href = $"api/categories/{category.Id}" });
            }
            
            return categories;
        }

        public CategoryDetailDto GetCategoryById(int id)
        {
            var category = _dbContext.Categories
                .Where(n => n.Id == id)
                .Select(n => new CategoryDetailDto
                {
                    Id = n.Id,
                    Name = n.Name,
                    Slug = n.Slug,
                    NumberOfNewsItems = _dbContext.CategoryNewsItem.Where(cni => cni.CategoriesId == id).Count()
                }).FirstOrDefault();

            if (category != null)
            {
                category.Links.AddReference("self", new { href = $"api/categories/{category.Id}" });
                category.Links.AddReference("edit", new { href = $"api/categories/{category.Id}" });
                category.Links.AddReference("delete", new { href = $"api/categories/{category.Id}" });
            }

            return category;
        }

        public bool LinkCategoryByCategoryIdAndNewsItemId(int categoryId, int newsItemId)
        {
            var categoryExists = _dbContext.Categories.Where(n => n.Id == categoryId).FirstOrDefault();
            var newsItemExists = _dbContext.NewsItems.Where(n => n.Id == newsItemId).FirstOrDefault();
            if(categoryExists == null || newsItemExists == null)
            {
                return false;
            }
            var category = _dbContext.Categories
                .Where(c => c.Id == categoryId)
                .FirstOrDefault();

            var newsItem = _dbContext.NewsItems
                .Where(ni => ni.Id == newsItemId)
                .FirstOrDefault();

            var categoryNewsItem = _dbContext.CategoryNewsItem
                .Where(cni => cni.CategoriesId == categoryId && cni.NewsItemsId == newsItemId)
                .FirstOrDefault();

            if (category != null && newsItem != null && categoryNewsItem == null)
            {
                _dbContext.CategoryNewsItem.Add(new CategoryNewsItem
                {
                    CategoriesId = categoryId,
                    NewsItemsId = newsItemId
                });

                _dbContext.SaveChanges();
            }         
            return true;   
        }

        public int CreateCategory(CategoryInputModel category)
        {
            var nextId = _dbContext.Categories.Max(table => table.Id) + 1;
            _dbContext.Categories.Add(new Category
            {
                // code generated
                Id = nextId,
                // from body
                Name = category.Name,
                
                
                // code generated
                Slug = category.Name.ToLower().Replace(" ", "-"),
                ModifiedBy = "David",
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
                
            });

            _dbContext.SaveChanges();

            return nextId;
        }

        public void UpdateCategory(int id, CategoryInputModel categoryData)
        {
            var singleCategory = _dbContext.Categories.FirstOrDefault(n => n.Id == id);
            
            singleCategory.Name = categoryData.Name;
            
            _dbContext.SaveChanges();
        }
        
        public void DeleteCategory(int id)
        {
            var category = _dbContext.Categories.Single(n => n.Id == id);
            _dbContext.Remove(category);
            _dbContext.SaveChanges();
        }
        
    }
}