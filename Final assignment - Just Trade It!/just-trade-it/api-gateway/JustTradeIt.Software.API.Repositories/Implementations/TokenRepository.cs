using JustTradeIt.Software.API.Repositories.Interfaces;
using JustTradeIt.Software.API.Models.Entities;
using JustTradeIt.Software.API.Repositories.Contexts;
using System.Linq;

namespace JustTradeIt.Software.API.Repositories.Implementations
{
    public class TokenRepository : ITokenRepository
    {
        private readonly JustTradeItDbContext _dbContext;

        public TokenRepository(JustTradeItDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public JwtToken CreateNewToken()
        {
            var token = new JwtToken();
            _dbContext.JwtTokens.Add(token);
            _dbContext.SaveChanges();
            return token;
        }

        public bool IsTokenBlacklisted(int tokenId)
        {
            var token = _dbContext.JwtTokens.FirstOrDefault(t => t.Id == tokenId);
            if (token == null) { return true; }
            return token.Blacklisted;
        }

        public void VoidToken(int tokenId)
        {
            var token = _dbContext.JwtTokens.FirstOrDefault(t => t.Id == tokenId);
            if (token == null) { return; }
            token.Blacklisted = true;
            _dbContext.SaveChanges();

        }
    }
}