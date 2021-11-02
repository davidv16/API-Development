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

namespace JustTradeIt.Software.API.Repositories.Implementations
{
    public class TradeRepository : ITradeRepository
    {
        private readonly JustTradeItDbContext _dbContext;

        public TradeRepository(JustTradeItDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public string CreateTradeRequest(string email, TradeInputModel trade)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public TradeDetailsDto UpdateTradeRequest(string email, string identifier, Models.Enums.TradeStatus newStatus)
        {
            throw new NotImplementedException();
        }
    }
}