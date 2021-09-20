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
    public class NewsItemRepository : INewsItemRepository
    {
        private readonly NewsDbContext _dbContext;

        public NewsItemRepository(NewsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<NewsItemDto> GetAllNewsItems()
        {
            var newsItems = _dbContext.NewsItems
                .OrderByDescending(n => n.PublishDate)
                .Select(n => new NewsItemDto
                {
                    Id = n.Id,
                    Title = n.Title,
                    ImgSource = n.ImgSource,
                    ShortDescription = n.ShortDescription
                }).ToList();

            // Adding HyperMedia
            foreach (var newsItem in newsItems)
            {
                newsItem.Links.AddReference("self", new { href = $"/api/{newsItem.Id}" });
                newsItem.Links.AddReference("edit", new { href = $"/api/{newsItem.Id}" });
                newsItem.Links.AddReference("delete", new { href = $"/api/{newsItem.Id}" });
                newsItem.Links.AddListReference("authors", _dbContext.AuthorNewsItem
                    .Where(ani => ani.NewsItemsId == newsItem.Id)
                    .Select(ani => new { href = $"api/authors/{ani.AuthorsId}" })
                );
                newsItem.Links.AddListReference("categories", _dbContext.CategoryNewsItem
                    .Where(cni => cni.NewsItemsId == newsItem.Id)
                    .Select(cni => new { href = $"api/categories/{cni.CategoriesId}" })
                );
            }
                
            return newsItems;
        }

        public NewsItemDetailDto GetNewsItemById(int id)
        {
            var newsItem = _dbContext.NewsItems
                .Where(n => n.Id == id)
                .Select(n => new NewsItemDetailDto
                {
                    Id = n.Id,
                    Title = n.Title,
                    ImgSource = n.ImgSource,
                    ShortDescription = n.ShortDescription,
                    LongDescription = n.LongDescription,
                    PublishDate = n.PublishDate
                })
                .FirstOrDefault();

            if (newsItem != null) {
                // Adding HyperMedia
                newsItem.Links.AddReference("self", new { href = $"/api/{newsItem.Id}" });
                newsItem.Links.AddReference("edit", new { href = $"/api/{newsItem.Id}" });
                newsItem.Links.AddReference("delete", new { href = $"/api/{newsItem.Id}" });
                newsItem.Links.AddListReference("authors", _dbContext.AuthorNewsItem
                    .Where(ani => ani.NewsItemsId == newsItem.Id)
                    .Select(ani => new { href = $"api/authors/{ani.AuthorsId}" })
                );
                newsItem.Links.AddListReference("categories", _dbContext.CategoryNewsItem
                    .Where(cni => cni.NewsItemsId == newsItem.Id)
                    .Select(cni => new { href = $"api/categories/{cni.CategoriesId}" })
                );
            }

            return newsItem;
        }

        public int CreateNewsItem(NewsItemInputModel newsItem)
        {
            var nextId = _dbContext.NewsItems.Max(table => table.Id) + 1;
            _dbContext.NewsItems.Add(new NewsItem
            {
                // code generated
                Id = nextId,
                // from body
                Title = newsItem.Title,
                ImgSource = newsItem.ImgSource,
                ShortDescription = newsItem.ShortDescription,
                LongDescription = newsItem.LongDescription,
                PublishDate = newsItem.PublishDate,
                // code generated
                ModifiedBy = "David",
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            });

            _dbContext.SaveChanges();

            return nextId;

        }

        public void UpdateNewsItem(int id, NewsItemInputModel newsItemData)
        {
            var singleNewsItem = _dbContext.NewsItems.FirstOrDefault(n => n.Id == id);
            

                singleNewsItem.Title = newsItemData.Title;
                singleNewsItem.ImgSource = newsItemData.ImgSource;
                singleNewsItem.ShortDescription = newsItemData.ShortDescription;
                singleNewsItem.LongDescription = newsItemData.LongDescription;
                singleNewsItem.PublishDate = newsItemData.PublishDate;
            


            _dbContext.SaveChanges();
        }
        
        public void DeleteNewsItem(int id)
        {
            var newsItem = _dbContext.NewsItems.Single(n => n.Id == id);
            _dbContext.Remove(newsItem);
            _dbContext.SaveChanges();
        }
    }
}