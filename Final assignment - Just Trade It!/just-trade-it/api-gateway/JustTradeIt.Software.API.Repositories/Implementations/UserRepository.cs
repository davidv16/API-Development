using System;
using JustTradeIt.Software.API.Repositories.Interfaces;
using JustTradeIt.Software.API.Models.InputModels;
using JustTradeIt.Software.API.Models.Dtos;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;
using JustTradeIt.Software.API.Repositories.Contexts;
using JustTradeIt.Software.API.Models.Entities;
using System.Linq;
using JustTradeIt.Software.API.Repositories.Helpers;
using Microsoft.AspNetCore.Http;

namespace JustTradeIt.Software.API.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly JustTradeItDbContext _dbContext;
        private readonly ITokenRepository _tokenRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string _salt = "00209b47-08d7-475d-a0fb-20abf0872ba0";

        public UserRepository(JustTradeItDbContext dbContext, ITokenRepository tokenRepository, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _tokenRepository = tokenRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public UserDto AuthenticateUser(LoginInputModel loginInputModel)
        {
            var user = _dbContext.Users.FirstOrDefault(u =>
                u.Email == loginInputModel.Email &&
                u.HashedPassword == HashHelper.HashPassword(loginInputModel.Password, _salt));
            if (user == null) { return null; }

            var token = _tokenRepository.CreateNewToken();

            return new UserDto
            {
                Identifier = user.PublicIdentifier,
                Email = user.Email,
                FullName = user.FullName,
                TokenId = token.Id
            };
        }

        public UserDto CreateUser(RegisterInputModel inputModel)
        {

            var user = _dbContext.Users.FirstOrDefault(u =>
                u.Email == inputModel.Email);
            if (!(user == null)) { throw new Exception("User already exists"); }

            //var nextId = (_dbContext.ItemConditions.Max(t => (int?)t.Id) ?? 0) + 1;
            var newIdentifier = Guid.NewGuid().ToString();

            _dbContext.Users.Add(new User
            {
                //Id = nextId,
                PublicIdentifier = newIdentifier,
                FullName = inputModel.FullName,
                Email = inputModel.Email,
                //TODO: figure out 
                ProfileImageUrl = "/someurl/",
                HashedPassword = HashHelper.HashPassword(inputModel.Password, _salt)
            });
            _dbContext.SaveChanges();

            var token = _tokenRepository.CreateNewToken();

            return new UserDto
            {
                Identifier = newIdentifier,
                FullName = inputModel.FullName,
                Email = inputModel.Email,
                TokenId = token.Id
            };
        }

        public UserDto GetProfileInformation(string email)
        {
            var user = _dbContext.Users.FirstOrDefault(u =>
                u.Email == email);

            int.TryParse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "tokenId").Value, out var tokenId);

            var profileInfo = new UserDto
            {
                Identifier = user.PublicIdentifier,
                FullName = user.FullName,
                Email = user.Email,
                ProfileImageUrl = user.ProfileImageUrl,
                TokenId = tokenId
            };

            return profileInfo;

        }

        public UserDto GetUserInformation(string userIdentifier)
        {
            var user = _dbContext.Users.FirstOrDefault(u =>
                u.PublicIdentifier == userIdentifier);

            int.TryParse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "tokenId").Value, out var tokenId);

            var userInfo = new UserDto
            {
                Identifier = user.PublicIdentifier,
                FullName = user.FullName,
                Email = user.Email,
                ProfileImageUrl = user.ProfileImageUrl,
                TokenId = tokenId

            };

            return userInfo;
        }

        public void UpdateProfile(string email, string profileImageUrl, ProfileInputModel profile)
        {
            var newProfile = _dbContext.Users.FirstOrDefault(n => n.Email == email);

            newProfile.FullName = profile.FullName;
            newProfile.ProfileImageUrl = profile.ProfileImage.ToString();

            _dbContext.SaveChanges();
        }

    }
}