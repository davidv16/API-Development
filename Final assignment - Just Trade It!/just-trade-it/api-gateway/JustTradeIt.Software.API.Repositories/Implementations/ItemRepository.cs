using System;
using JustTradeIt.Software.API.Models.InputModels;
using JustTradeIt.Software.API.Models.Dtos;
using JustTradeIt.Software.API.Models;
using JustTradeIt.Software.API.Repositories.Interfaces;
using JustTradeIt.Software.API.Repositories.Contexts;
using System.Linq;

namespace JustTradeIt.Software.API.Repositories.Implementations
{
    public class ItemRepository : IItemRepository
    {
        private readonly JustTradeItDbContext _dbContext;

        public ItemRepository(JustTradeItDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string AddNewItem(string email, ItemInputModel item)
        {
            throw new NotImplementedException();
        }

        public Envelope<ItemDto> GetAllItems(int pageSize, int pageNumber, bool ascendingSortOrder)
        {
            var item = _dbContext.Items
                .Select(n => new ItemDto
                {
                    Identifier = n.PublicIdentifier,
                    Title = n.Title,
                    ShortDescription = n.ShortDescription,
                    Owner = new UserDto
                    {
                    }
                });

            return item;
        }

        public ItemDetailsDto GetItemByIdentifier(string identifier)
        {
            throw new NotImplementedException();
        }

        public void RemoveItem(string email, string identifier)
        {
            throw new NotImplementedException();
        }
    }
}