using System.Collections.Generic;
using JustTradeIt.Software.API.Services.Interfaces;
using JustTradeIt.Software.API.Models.Dtos;

namespace JustTradeIt.Software.API.Services.Implementations
{
    public class UserService : IUserService
    {
        public UserDto GetUserInformation(string identifier)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<TradeDto> GetUserTrades(string userIdentifier)
        {
            throw new System.NotImplementedException();
        }
    }
}