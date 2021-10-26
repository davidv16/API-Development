using System.Threading.Tasks;
using JustTradeIt.Software.API.Services.Interfaces;
using JustTradeIt.Software.API.Models.InputModels;
using JustTradeIt.Software.API.Models.Dtos;
using JustTradeIt.Software.API.Repositories.Interfaces;

namespace JustTradeIt.Software.API.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenRepository _tokenRepository;

        public AccountService(IUserRepository userRepository, ITokenRepository tokenRepository)
        {
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
        }

        public UserDto AuthenticateUser(LoginInputModel loginInputModel)
        {
            return _userRepository.AuthenticateUser(loginInputModel);
        }

        //TODO: implement
        public UserDto CreateUser(RegisterInputModel inputModel)
        {
            throw new System.NotImplementedException();
        }

        //TODO: implement
        public UserDto GetProfileInformation(string name)
        {
            throw new System.NotImplementedException();
        }

        public void Logout(int tokenId)
        {
            _tokenRepository.VoidToken(tokenId);
        }

        //TODO: implement
        public Task UpdateProfile(string email, ProfileInputModel profile)
        {
            throw new System.NotImplementedException();
        }
    }
}