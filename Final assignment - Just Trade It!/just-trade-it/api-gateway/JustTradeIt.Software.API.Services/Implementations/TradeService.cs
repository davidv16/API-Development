using System;
using System.Collections.Generic;
using System.Linq;
using JustTradeIt.Software.API.Services.Interfaces;
using JustTradeIt.Software.API.Models.InputModels;
using JustTradeIt.Software.API.Models.Dtos;
using JustTradeIt.Software.API.Repositories.Interfaces;
using JustTradeIt.Software.API.Models.Enums;

namespace JustTradeIt.Software.API.Services.Implementations
{
    public class TradeService : ITradeService
    {
        private readonly ITradeRepository _tradeRepository;
        private readonly IQueueService _queueService;

        public TradeService(ITradeRepository tradeRepository, IQueueService queueService)
        {
            _tradeRepository = tradeRepository;
            _queueService = queueService;
        }

        public IEnumerable<TradeDto> GetTrades(string email)
        {
            // Gets all successful trades for a particular user
            return _tradeRepository.GetTrades(email);
        }

        public TradeDetailsDto GetTradeByIdentifer(string tradeIdentifier)
        {
            //Gets a detailed representation of a trade
            return _tradeRepository.GetTradeByIdentifier(tradeIdentifier);
        }

        public IEnumerable<TradeDto> GetTradeRequests(string email, bool onlyIncludeActive = true)
        {
            //Get all trade requests of the authenticated user
            return _tradeRepository.GetTradeRequests(email, onlyIncludeActive);
        }

        public string CreateTradeRequest(string email, TradeInputModel tradeRequest)
        {
            //Create a new trade request
            //TODO: Publish a message to RabbitMQ with the routing key
            //‘new-trade-request’ and include the required data
            var identifier = _tradeRepository.CreateTradeRequest(email, tradeRequest);
            var trade = _tradeRepository.GetTradeByIdentifier(identifier);
            _queueService.PublishMessage("new-trade-request", new { trade.Receiver.Email });

            return identifier;
        }

        public void UpdateTradeRequest(string identifier, string email, string status)
        {
            //Update the status of the trade request. Trade requests can only be
            //changed if not in a finalized state
            //TODO: Publish a message to RabbitMQ with the routing key
            //‘trade-update-request’ and include the required data
            var trade = _tradeRepository.UpdateTradeRequest(email, identifier, Enum.Parse<TradeStatus>(status));
            _queueService.PublishMessage("trade-update-request", new
            {
                trade.Receiver.Email,
                ReceivingItems = trade.ReceivingItems
                    .Select(i => new {
                        i.Title,
                        i.ShortDescription,
                    }),
                OfferingItems = trade.OfferingItems
                    .Select(i => new {
                        i.Title,
                        i.ShortDescription,
                    }),
                Sender = new{
                    trade.Sender.Email,
                    trade.Sender.FullName,
                },
                trade.IssuedDate,
                trade.ModifiedDate,
                trade.Status,
            });
        }
    }
}