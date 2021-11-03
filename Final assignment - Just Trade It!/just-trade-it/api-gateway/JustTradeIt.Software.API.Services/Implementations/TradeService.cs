using System;
using System.Collections.Generic;
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

        public TradeService(ITradeRepository tradeRepository)
        {
            _tradeRepository = tradeRepository;
        }

        public string CreateTradeRequest(string email, TradeInputModel tradeRequest)
        {
            return _tradeRepository.CreateTradeRequest(email, tradeRequest);
        }

        public TradeDetailsDto GetTradeByIdentifer(string tradeIdentifier)
        {
            return _tradeRepository.GetTradeByIdentifier(tradeIdentifier);
        }

        public IEnumerable<TradeDto> GetTradeRequests(string email, bool onlyIncludeActive = true)
        {
            return _tradeRepository.GetTradeRequests(email, onlyIncludeActive);
        }

        public IEnumerable<TradeDto> GetTrades(string email)
        {
            return _tradeRepository.GetTrades(email);
        }

        public void UpdateTradeRequest(string identifier, string email, string status)
        {
            _tradeRepository.UpdateTradeRequest(email, identifier, Enum.Parse<TradeStatus>(status));
        }
    }
}