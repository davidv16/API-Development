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
            var nextId = _dbContext.Items.Max(table => table.Id) + 1;
            var newIdentifier = Guid.NewGuid().ToString();
            _dbContext.Items.Add(new Item
            {
                Id = nextId,
                PublicIdentifier = newIdentifier,
                Title = item.Title,
                Description = item.Description,
                ShortDescription = item.ShortDescription,
                OwnerId = _dbContext.Users.FirstOrDefault(n => n.Email == email).Id
            });

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
                
            var envelope = new Envelope<ItemDto>(pageNumber, pageSize, items);
            return envelope;
        }

        public ItemDetailsDto GetItemByIdentifier(string identifier)
        {
            int.TryParse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "tokenId").Value, out var tokenId);
            
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
                    //TODO: what goes here?
                    NumberOfActiveTradeRequests = n.TradeItems.Count(),
                    //TODO: what to put here?
                    Condition = "dfa",
                    Owner = new UserDto 
                    {
                        Identifier = n.Owner.PublicIdentifier,
                        FullName = n.Owner.FullName,
                        Email = n.Owner.Email,
                        ProfileImageUrl = n.Owner.ProfileImageUrl,
                        TokenId = tokenId
                    }
                }).FirstOrDefault();

            return item;
        }

        public void RemoveItem(string email, string identifier)
        {
            var owner = _dbContext.Items.FirstOrDefault(n => n.Owner.Email == email);
            if(owner == null){throw new Exception("This Item doesn't have an owner with that email address");}

            var item = _dbContext.Items.Single(n => n.PublicIdentifier == identifier);
            _dbContext.Remove(item);
            _dbContext.SaveChanges();
        }
    }
}