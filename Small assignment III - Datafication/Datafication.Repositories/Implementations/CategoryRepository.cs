using System;
using System.Collections.Generic;
using System.Linq;
using Datafication.Models.Dtos;
using Datafication.Models.InputModels;
using Datafication.Repositories.Contexts;
using Datafication.Repositories.Entities;
using Datafication.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Datafication.Repositories.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IceCreamDbContext _dbContext;

        public CategoryRepository(IceCreamDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        //TODO: Finish
        public IEnumerable<IceCreamDto> GetIceCreamsByCategoryId(int id) 
        {
            
            var IceCream = _dbContext.Categories
                .Include(ic => ic.IceCreams)
                .Where(ic => ic.Id == id)
                .Select(i => new IceCreamDto
                {
                    //Id = i.IceCreams.Id,
                    //Name = i.IceCreams.Name,
                    //Description = i.IceCreams.Description
                });
            
            return IceCream.ToList();
            
        }
        
        public int CreateNewCategory(CategoryInputModel category) 
        {
            var nextId = _dbContext.Categories.Max(table => table.Id) + 1;
            _dbContext.Categories.Add(new Category
            {
                Id = nextId,
                Name = category.Name,
                ParentCategoryId = category.ParentCategoryId
            });

            _dbContext.SaveChanges();
            return nextId;
        }
        
        public void DeleteCategory(int id) 
        {
            var category = _dbContext.Categories.Single(n => n.Id == id);
            _dbContext.Remove(category);
            _dbContext.SaveChanges();
        }
        
    }
}