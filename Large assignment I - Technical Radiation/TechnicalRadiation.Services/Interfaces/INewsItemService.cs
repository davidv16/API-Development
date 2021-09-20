using System.Collections.Generic;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.InputModels;

namespace TechnicalRadiation.Services.Interfaces
{
    public interface INewsItemService
    {
        IEnumerable<NewsItemDto> GetAllNewsItems();
        NewsItemDetailDto GetNewsItemById(int id);
        int CreateNewsItem(NewsItemInputModel newsItem);
        void UpdateNewsItem(int id, NewsItemInputModel newsItemData);
        void DeleteNewsItem(int id);
    }
}