using System;
using System.Collections.Generic;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.Extensions;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Repositories.Interfaces;
using TechnicalRadiation.Services.Interfaces;

namespace TechnicalRadiation.Services.Implementations
{
    public class NewsItemService : INewsItemService
    {
        private readonly INewsItemRepository _newsItemRepository;

        public NewsItemService(INewsItemRepository newsItemRepository)
        {
            _newsItemRepository = newsItemRepository;
        }

        public IEnumerable<NewsItemDto> GetAllNewsItems() {
            var newsItems = _newsItemRepository.GetAllNewsItems();

            return newsItems;
        }

        public NewsItemDetailDto GetNewsItemById(int id)
        {
            var newsItem = _newsItemRepository.GetNewsItemById(id);

            return newsItem;
        }

        public int CreateNewsItem(NewsItemInputModel newsItem)
        {
            return _newsItemRepository.CreateNewsItem(newsItem);
        }

        public void UpdateNewsItem(int id, NewsItemInputModel newsItemData)
        {
            _newsItemRepository.UpdateNewsItem(id, newsItemData);
        }

        public void DeleteNewsItem(int id)
        {
            _newsItemRepository.DeleteNewsItem(id);
        }
    }
}