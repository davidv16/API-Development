using System;
using JustTradeIt.Software.API.Models.InputModels;
using JustTradeIt.Software.API.Models.Dtos;
using JustTradeIt.Software.API.Models;
using JustTradeIt.Software.API.Repositories.Interfaces;
using JustTradeIt.Software.API.Repositories.Contexts;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using JustTradeIt.Software.API.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

   
using System.Collections.Generic;
//using Microsoft.AspNetCore.Authorization;

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
            var item = _dbContext.Items
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
            //TODO: figure out how to return
            throw new NotImplementedException();
        }

        public ItemDetailsDto GetItemByIdentifier(string identifier)
        {
            var item = _dbContext.Items
                .Where(n => n.PublicIdentifier == identifier)
                .Include(i => i.ItemImages)
                .Include(u => u.Owner)
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
                    NumberOfActiveTradeRequests = 2,
                    Condition = "dfa",
                    Owner = new UserDto 
                    {
                        Identifier = n.Owner.PublicIdentifier,
                        FullName = n.Owner.FullName,
                        Email = n.Owner.Email,
                        ProfileImageUrl = n.Owner.ProfileImageUrl,
                        //TODO: figure out how to return tokenid?
                        //TokenId = n.Owner.

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