using System;
using System.Collections.Generic;
using JustTradeIt.Software.API.Repositories.Interfaces;
using JustTradeIt.Software.API.Models.InputModels;
using JustTradeIt.Software.API.Models.Dtos;
using JustTradeIt.Software.API.Repositories.Contexts;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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
            /* Create a new trade request */

            var nextId = (_dbContext.Trades.Max(t => (int?)t.Id) ?? 0) + 1;
            var newIdentifier = Guid.NewGuid().ToString();

            var sender = _dbContext.Users.FirstOrDefault(s => s.Email == email);
            var receiver = _dbContext.Users.FirstOrDefault(r => r.PublicIdentifier == trade.ReceiverIdentifier);
            /* Both users must be valid users within the application */
            if (receiver == null) { throw new Exception($"No receiver found"); }

            //Map items in trade to trade items
            var itemsInTrade = trade.ItemsInTrade
                .Select(t => _dbContext.Items.FirstOrDefault(i => i.PublicIdentifier == t.Identifier));
            foreach (var item in itemsInTrade)
            {
                if (item.deleted == true) { throw new Exception("Deleted items are non tradeable"); }
            }

            //List of Sender Items
            var senderItems = itemsInTrade.Where(s => s.OwnerId == sender.Id);
            /* When a user creates a trade request he can only put his own items as
            part of the trade request */
            if (senderItems.Count() == 0) { throw new Exception("sender doesn't own an item in the trade"); }

            //List of Reciever Items
            var receiverItems = itemsInTrade.Where(r => r.OwnerId == receiver.Id);
            /* The user which he is trying to trade with must also own the items
            proposed in the trade request */
            if (receiverItems.Count() == 0) { throw new Exception("reciever doesn't own an item in the trade"); }

            //List of items in the trade that are neither owned by the sender or receiver
            var orphanItems = itemsInTrade
                .Where(i => !(receiver.Id == i.OwnerId) && !(sender.Id == i.OwnerId));
            if (orphanItems.Count() != 0) { throw new Exception("Some items in the trade don't belong to either sender or receiver"); }


            _dbContext.Trades.Add(new Trade
            {
                Id = nextId,
                PublicIdentifier = newIdentifier,
                IssueDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                ModifiedBy = email,
                TradeStatus = TradeStatus.Pending.ToString(),
                ReceiverId = receiver.Id,
                SenderId = sender.Id,
            });

            _dbContext.TradeItems.AddRange(itemsInTrade
                .Select(i => new TradeItem
                {
                    TradeId = nextId,
                    UserId = i.OwnerId,
                    ItemId = i.Id
                }));


            _dbContext.SaveChanges();

            return newIdentifier;
        }

        public TradeDetailsDto GetTradeByIdentifier(string identifier)
        {
            //Get a trade by an identifier. This should return a detailed
            //representation of the trade
            var trade = _dbContext.Trades
                .Where(n => n.PublicIdentifier == identifier)
                .Include(i => i.TradeItems)
                .Include(s => s.Sender)
                .Include(r => r.Receiver)
                .Select(n => new TradeDetailsDto
                {
                    Identifier = n.PublicIdentifier,
                    ReceivingItems = n.TradeItems
                        .Where(ti => ti.TradeId == n.Id && ti.UserId == n.ReceiverId)
                        .Select(ti => new ItemDto
                        {
                            Identifier = ti.Item.PublicIdentifier,
                            Title = ti.Item.Title,
                            ShortDescription = ti.Item.ShortDescription,
                            Owner = new UserDto
                            {
                                Identifier = ti.Item.Owner.PublicIdentifier,
                                FullName = ti.Item.Owner.FullName,
                                Email = ti.Item.Owner.Email,
                                ProfileImageUrl = ti.Item.Owner.ProfileImageUrl
                            }
                        }),
                    OfferingItems = n.TradeItems
                        .Where(ti => ti.TradeId == n.Id && ti.UserId == n.SenderId)
                        .Select(ti => new ItemDto
                        {
                            Identifier = ti.Item.PublicIdentifier,
                            Title = ti.Item.Title,
                            ShortDescription = ti.Item.ShortDescription,
                            Owner = new UserDto
                            {
                                Identifier = ti.Item.Owner.PublicIdentifier,
                                FullName = ti.Item.Owner.FullName,
                                Email = ti.Item.Owner.Email,
                                ProfileImageUrl = ti.Item.Owner.ProfileImageUrl
                            }
                        }),
                    Receiver = new UserDto
                    {
                        Identifier = n.Receiver.PublicIdentifier,
                        FullName = n.Receiver.FullName,
                        Email = n.Receiver.Email,
                        ProfileImageUrl = n.Receiver.ProfileImageUrl
                    },
                    Sender = new UserDto
                    {
                        Identifier = n.Sender.PublicIdentifier,
                        FullName = n.Sender.FullName,
                        Email = n.Sender.Email,
                        ProfileImageUrl = n.Sender.ProfileImageUrl
                    },
                    ReceivedDate = n.TradeStatus == TradeStatus.Accepted.ToString() ? n.ModifiedDate : null,
                    IssuedDate = n.IssueDate,
                    ModifiedDate = n.ModifiedDate,
                    ModifiedBy = n.ModifiedBy,
                    Status = n.TradeStatus
                }).FirstOrDefault();

            if (trade == null) { throw new Exception("Trade was not found"); }

            return trade;
        }

        public IEnumerable<TradeDto> GetTradeRequests(string email, bool onlyIncludeActive)
        {
            //Get all trade requests associated with the authenticated user. A trade
            //request is a trade, before it reaches an accepted state. Therefore all
            //states excluding ‘Accepted’ are considered a trade request
            var activeTradeRequests = _dbContext.Trades
                .Where(n => (n.Sender.Email == email) || (n.Receiver.Email == email))
                .Where(n => n.TradeStatus != TradeStatus.Accepted.ToString())
                .Where(n => n.TradeStatus == TradeStatus.Pending.ToString())
                .Select(n => new TradeDto
                {
                    Identifier = n.PublicIdentifier,
                    IssuedDate = n.IssueDate,
                    ModifiedDate = n.ModifiedDate,
                    Status = n.TradeStatus
                }).ToList();

            //Only returns active trade requests
            if (onlyIncludeActive) { return activeTradeRequests; }

            var tradeRequests = _dbContext.Trades
                .Where(n => (n.Sender.Email == email) || (n.Receiver.Email == email))
                .Where(n => n.TradeStatus != TradeStatus.Accepted.ToString())
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
            //Get all completed trades associated with the authenticated user. A
            //trade is considered completed if it is in the ‘Accepted’ state

            var acceptedTrades = _dbContext.Trades
                .Where(n => (n.Sender.Email == email) || (n.Receiver.Email == email))
                .Where(n => n.TradeStatus == TradeStatus.Accepted.ToString())
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
            /* Gets all completed trades by user identifier */
            /* The user can either be the initiator of the trade or the receiver */

            var userTrades = _dbContext.Trades
                .Include(r => r.Receiver)
                .Include(s => s.Sender)
                .Where(n => n.TradeStatus == TradeStatus.Accepted.ToString())
                .Where(n => (n.Sender.PublicIdentifier == userIdentifier) || (n.Receiver.PublicIdentifier == userIdentifier))
                .Select(n => new TradeDto
                {
                    Identifier = n.PublicIdentifier,
                    IssuedDate = n.IssueDate,
                    ModifiedDate = n.ModifiedDate,
                    Status = n.TradeStatus
                }).ToList();

            if (userTrades == null) { return null; }
            return userTrades;
        }

        public TradeDetailsDto UpdateTradeRequest(string email, string identifier, Models.Enums.TradeStatus newStatus)
        {
            /* Updates a trade request status. This is done to either cancel, decline
            or accept an active trade request */

            var user = _dbContext.Users.FirstOrDefault(n => n.Email == email);
            var tradeRequest = _dbContext.Trades
                .Where(n => (n.PublicIdentifier == identifier))
                .Where(u => (u.SenderId == user.Id || u.ReceiverId == user.Id))
                .FirstOrDefault();

            if (tradeRequest == null) { throw new Exception("Only a participant of the trade can update the status."); }
            /* The only suitable trade request status prior to the update is the
            ‘Pending’ state. If the trade request is not in the ‘Pending’ state an exception should be thrown */
            if (tradeRequest.TradeStatus != TradeStatus.Pending.ToString()) { throw new Exception("Trade request is already complete."); }


            /* If the user is the initiator of the trade, he can only cancel the trade
            request */
            if (tradeRequest.Sender.Email == email && newStatus != TradeStatus.Cancelled)
            {
                throw new Exception("You can only cancel.");
            }

            /* If the user is the receiver in the trade, he can either accept or decline
            the trade request */
            if (tradeRequest.Receiver.Email == email && (newStatus != TradeStatus.Accepted && newStatus != TradeStatus.Declined))
            {
                throw new Exception("You can only either accept or decline");
            }

            tradeRequest.TradeStatus = newStatus.ToString();

            _dbContext.SaveChanges();

            return GetTradeByIdentifier(identifier);

        }
    }
}