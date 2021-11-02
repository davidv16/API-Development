using System;
using System.Collections.Generic;
using JustTradeIt.Software.API.Repositories.Interfaces;
using JustTradeIt.Software.API.Models.InputModels;
using JustTradeIt.Software.API.Models.Dtos;
using JustTradeIt.Software.API.Repositories.Contexts;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;
using Microsoft.EntityFrameworkCore.Query.Internal;
using JustTradeIt.Software.API.Models.Entities;
using JustTradeIt.Software.API.Models.Enums;
using Microsoft.AspNetCore.Http;

namespace JustTradeIt.Software.API.Repositories.Implementations
{
    public class TradeRepository : ITradeRepository
    {
        private readonly JustTradeItDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TradeRepository(JustTradeItDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }
        public string CreateTradeRequest(string email, TradeInputModel trade)
        {
            var nextId = _dbContext.Trades.Max(table => table.Id) + 1;
            var newIdentifier = Guid.NewGuid().ToString();
            
            _dbContext.Trades.Add(new Trade
            {
                Id = nextId,
                PublicIdentifier = newIdentifier,
                IssueDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                ModifiedBy = email,
                TradeStatus = TradeStatus.Pending.ToString(),
                //TODO: figure out 
                ReceiverId = 0,
                SenderId = _dbContext.Users.FirstOrDefault(n => n.Email == email).Id,
                //TODO: figure out Trade Items
                //TradeItems = trade.ItemsInTrade
            });

            _dbContext.SaveChanges();

            return newIdentifier;
        }

        public TradeDetailsDto GetTradeByIdentifier(string identifier)
        {
            var trade = _dbContext.Trades
                .Where(n => n.PublicIdentifier == identifier)
                .Include(i => i.TradeItems)
                .Select(n => new TradeDetailsDto
                {
                    Identifier = n.PublicIdentifier,
                    //TODO: Receiving items
                    //TODO: Offering Items
                    Receiver = new UserDto
                    {
                        Identifier = n.Receiver.PublicIdentifier,
                        FullName = n.Receiver.FullName,
                        Email = n.Receiver.Email,
                        ProfileImageUrl = n.Receiver.ProfileImageUrl,
                        //TODO: TokenId
                    },
                    Sender = new UserDto
                    {
                        Identifier = n.Sender.PublicIdentifier,
                        FullName = n.Sender.FullName,
                        Email = n.Sender.Email,
                        ProfileImageUrl = n.Sender.ProfileImageUrl,
                        //TODO: TokenId
                    },
                    //TODO: ReceivedDate = n.RecievedDate
                    IssuedDate = n.IssueDate,
                    ModifiedDate = n.ModifiedDate,
                    ModifiedBy = n.ModifiedBy,
                    Status = n.TradeStatus
                }).FirstOrDefault();
            
            return trade;
        }

        public IEnumerable<TradeDto> GetTradeRequests(string email, bool onlyIncludeActive)
        {
            var activeTradeRequests = _dbContext.Trades
                .Where(n => (n.Sender.Email == email) || (n.Receiver.Email == email))
                .Where(n => n.TradeStatus != "Accepted")
                .Where(n => n.TradeStatus == "Active")
                .Select(n => new TradeDto
                {
                    Identifier = n.PublicIdentifier,
                    IssuedDate = n.IssueDate,
                    ModifiedDate = n.ModifiedDate,
                    Status = n.TradeStatus
                }).ToList();
            
            //Only returns active trade requests
            if(onlyIncludeActive) {return activeTradeRequests;}

            var tradeRequests = _dbContext.Trades
                .Where(n => (n.Sender.Email == email) || (n.Receiver.Email == email))
                .Where(n => n.TradeStatus != "Accepted")
                .Select(n => new TradeDto
                {
                    Identifier = n.PublicIdentifier,
                    IssuedDate = n.IssueDate,
                    ModifiedDate = n.ModifiedDate,
                    Status = n.TradeStatus
                }).ToList();
            return tradeRequests;
        }

        public IEnumerable<TradeDto> GetTrades(string email)
        {
            var acceptedTrades = _dbContext.Trades
                .Where(n => (n.Sender.Email == email) || (n.Receiver.Email == email))
                .Where(n => n.TradeStatus == "Accepted")
                .Select(n => new TradeDto
                {
                    Identifier = n.PublicIdentifier,
                    IssuedDate = n.IssueDate,
                    ModifiedDate = n.ModifiedDate,
                    Status = n.TradeStatus
                }).ToList();
            
            return acceptedTrades;
        }

        public IEnumerable<TradeDto> GetUserTrades(string userIdentifier)
        {
            var userTrades = _dbContext.Trades
                .Where(n => (n.Sender.PublicIdentifier == userIdentifier) || (n.Receiver.PublicIdentifier == userIdentifier))
                .Select(n => new TradeDto
                {
                    Identifier = n.PublicIdentifier,
                    IssuedDate = n.IssueDate,
                    ModifiedDate = n.ModifiedDate,
                    Status = n.TradeStatus
                }).ToList();
            
            return userTrades;
        }

        public TradeDetailsDto UpdateTradeRequest(string email, string identifier, Models.Enums.TradeStatus newStatus)
        {
            var user = _dbContext.Users.FirstOrDefault(n => n.Email == email);
            var tradeRequest = _dbContext.Trades
                .Where(n => (n.PublicIdentifier == identifier))
                .Where(u => (u.SenderId == user.Id || u.ReceiverId == user.Id))
                .FirstOrDefault();
            
            if(tradeRequest == null) {throw new Exception("Only a participant of the trade can update the status");}


            tradeRequest.TradeStatus = newStatus.ToString();

            _dbContext.SaveChanges();

            return new TradeDetailsDto
            {
                Identifier = tradeRequest.PublicIdentifier,
                //TODO: figure out
                //ReceivingItems = tradeRequest.ReceivingItems,
                //OfferingItems = tradeRequest.OfferingItems,
                Receiver = new UserDto
                    {
                        Identifier = tradeRequest.Receiver.PublicIdentifier,
                        FullName = tradeRequest.Receiver.FullName,
                        Email = tradeRequest.Receiver.Email,
                        ProfileImageUrl = tradeRequest.Receiver.ProfileImageUrl,
                        //TODO: TokenId
                    },
                Sender = new UserDto
                {
                    Identifier = tradeRequest.Sender.PublicIdentifier,
                    FullName = tradeRequest.Sender.FullName,
                    Email = tradeRequest.Sender.Email,
                    ProfileImageUrl = tradeRequest.Sender.ProfileImageUrl,
                    //TODO: TokenId
                },
                //TODO: figure out
                //ReceivedDate = tradeRequest.ReceivedDate,
                IssuedDate = tradeRequest.IssueDate,
                ModifiedDate = tradeRequest.ModifiedDate,
                ModifiedBy = tradeRequest.ModifiedBy,
                Status = tradeRequest.TradeStatus
            };

        }
    }
}