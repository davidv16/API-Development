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
    public class IceCreamRepository : IIceCreamRepository
    {
        private readonly IceCreamDbContext _dbContext;
        public IceCreamRepository(IceCreamDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<IceCreamDto> GetAllIceCreams() 
        {
            var IceCreams = _dbContext.IceCreams.Select(i => new IceCreamDto
            {
                Id = i.Id,
                Name = i.Name,
                Description = i.Description
            }).ToList();

            return IceCreams;
        }
        
        public IceCreamDetailsDto GetIceCreamById(int id) 
        {
            var IceCream = _dbContext
                .IceCreams
                .Include(i => i.Images)
                .Include(i => i.Categories)
                .Include(ma => ma.Manufacturer)
                .Where(i => i.Id == id)
                .Select(i => new IceCreamDetailsDto
                {
                    Id = i.Id,
                    Name = i.Name,
                    Description = i.Description,
                    Images = i.Images.Select(im => new ImageDto
                    {
                        Id = im.Id,
                        Url = im.Url
                    }),
                   Manufacturer = new ManufacturerDto
                    {
                        Id = i.Manufacturer.Id,
                        Name = i.Manufacturer.Name,
                        ExternalUrl = i.Manufacturer.ExternalUrl
                    },
                    Categories = i.Categories.Select(ca => new CategoryDto
                    {
                        Id = ca.Id,
                        Name = ca.Name,
                        ParentCategoryId = ca.ParentCategoryId

                    })
                });
            return IceCream.FirstOrDefault();
        }
        
        public int CreateNewIceCream(IceCreamInputModel iceCream) 
        {
            var nextId = _dbContext.IceCreams.Max(table => table.Id) + 1;
            _dbContext.IceCreams.Add(new IceCream
            {
                Id = nextId,
                Name = iceCream.Name,
                Description = iceCream.Description,
                ManufacturerId = iceCream.ManufacturerId,
            });

            _dbContext.SaveChanges();
            return nextId;
        }
        public void UpdateIceCream(int id, IceCreamInputModel iceCream) 
        {
            var NewIceCream = _dbContext.IceCreams.FirstOrDefault(n => n.Id == id);
            
            NewIceCream.Name = iceCream.Name;
            NewIceCream.Description = iceCream.Description;
            NewIceCream.ManufacturerId = iceCream.ManufacturerId;
            
            _dbContext.SaveChanges();
        }
        
        public void DeleteIceCream(int id) 
        {
            var IceCream = _dbContext.IceCreams.Single(n => n.Id == id);
            _dbContext.Remove(IceCream);
            _dbContext.SaveChanges();
        }
        
        //TODO: Finish
        public void AddIceCreamToCategory(int iceCreamId, int categoryId) 
        {
          /*  var iceCream = _dbContext.IceCreams
                .Where(a => a.Id == iceCreamId)
                .FirstOrDefault();

            var category = _dbContext.Categories
                .Where(ni => ni.Id == categoryId)
                .FirstOrDefault();
            
            _dbContext.Categories.Where(ni => ni.Id == categoryId).Add(new IceCream
            {
                IceCreams = iceCream
            });
            */
            /*

            var IceCreamCategoryItem = _dbContext.AuthorNewsItem
                .Where(ani => ani.AuthorsId == authorId && ani.NewsItemsId == newsItemId)
                .FirstOrDefault();

            if (author != null && newsItem != null && authorNewsItem == null)
            {
                _dbContext.AuthorNewsItem.Add(new AuthorNewsItem
                {
                    AuthorsId = authorId,
                    NewsItemsId = newsItemId
                });

            */
                _dbContext.SaveChanges();
            
        }
        
    }
}