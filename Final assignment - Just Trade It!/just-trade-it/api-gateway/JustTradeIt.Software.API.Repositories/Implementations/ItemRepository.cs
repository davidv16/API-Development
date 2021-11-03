using System;
using JustTradeIt.Software.API.Models.InputModels;
using JustTradeIt.Software.API.Models.Dtos;
using JustTradeIt.Software.API.Models;
using JustTradeIt.Software.API.Repositories.Interfaces;
using JustTradeIt.Software.API.Repositories.Contexts;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using JustTradeIt.Software.API.Models.Entities;
using Microsoft.AspNetCore.Http;

namespace JustTradeIt.Software.API.Repositories.Implementations
{
    public class ItemRepository : IItemRepository
    {
        private readonly JustTradeItDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ItemRepository(JustTradeItDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public string AddNewItem(string email, ItemInputModel item)
        {
            var itemConNewId = (_dbContext.ItemConditions.Max(t => (int?)t.Id) ?? 0) + 1;

            _dbContext.ItemConditions.Add(new ItemCondition
            {
                Id = itemConNewId,
                ConditionCode = item.ConditionCode
            });

            var itemNextId = (_dbContext.Items.Max(t => (int?)t.Id) ?? 0) + 1;
            var newIdentifier = Guid.NewGuid().ToString();

            _dbContext.Items.Add(new Item
            {
                Id = itemNextId,
                PublicIdentifier = newIdentifier,
                Title = item.Title,
                Description = item.Description,
                ShortDescription = item.ShortDescription,
                ItemConditionId = itemConNewId,
                OwnerId = _dbContext.Users.FirstOrDefault(n => n.Email == email).Id
            });

            _dbContext.ItemImages.AddRange(
                item.ItemImages
                    .Select(url => new ItemImage
                    {
                        ImageUrl = url,
                        ItemId = itemNextId
                    }));

            _dbContext.SaveChanges();

            return newIdentifier;
        }

        public Envelope<ItemDto> GetAllItems(int pageSize, int pageNumber, bool ascendingSortOrder)
        {
            var items = _dbContext.Items
                .Select(n => new ItemDto
                {
                    Identifier = n.PublicIdentifier,
                    Title = n.Title,
                    ShortDescription = n.ShortDescription,
                    Owner = new UserDto
                    {
                        Identifier = n.Owner.PublicIdentifier,
                        FullName = n.Owner.FullName,
                        Email = n.Owner.Email,
                        ProfileImageUrl = n.Owner.ProfileImageUrl
                    }
                }).ToList();

            //TODO: finish returning sorted items
            var envelope = new Envelope<ItemDto>(pageNumber, pageSize, items);
            return envelope;
        }

        public ItemDetailsDto GetItemByIdentifier(string identifier)
        {
            var item = _dbContext.Items
                .Where(n => n.PublicIdentifier == identifier)
                .Include(i => i.ItemImages)
                .Include(u => u.TradeItems)
                .Select(n => new ItemDetailsDto
                {
                    Identifier = n.PublicIdentifier,
                    Title = n.Title,
                    Description = n.Description,
                    Images = n.ItemImages.Select(n => new ImageDto
                    {
                        Id = n.Id,
                        ImageUrl = n.ImageUrl
                    }),
                    NumberOfActiveTradeRequests = n.TradeItems.Count(), //TODO: count number of active trade requests
                    Condition = n.ItemCondition.ConditionCode,
                    Owner = new UserDto
                    {
                        Identifier = n.Owner.PublicIdentifier,
                        FullName = n.Owner.FullName,
                        Email = n.Owner.Email,
                        ProfileImageUrl = n.Owner.ProfileImageUrl,
                    }
                }).FirstOrDefault();

            return item;
        }

        public void RemoveItem(string email, string identifier)
        {
            var owner = _dbContext.Items.FirstOrDefault(n => n.Owner.Email == email);
            if (owner == null) { throw new Exception("This Item doesn't have an owner with that email address"); }

            var item = _dbContext.Items.Single(n => n.PublicIdentifier == identifier);
            _dbContext.Remove(item);
            _dbContext.SaveChanges();
        }
    }
}