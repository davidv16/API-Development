using System;
using JustTradeIt.Software.API.Repositories.Interfaces;
using JustTradeIt.Software.API.Models.InputModels;
using JustTradeIt.Software.API.Models.Dtos;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;
using JustTradeIt.Software.API.Repositories.Contexts;
using JustTradeIt.Software.API.Models.Entities;
using System.Linq;

namespace JustTradeIt.Software.API.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly JustTradeItDbContext _dbContext;
        private readonly ITokenRepository _tokenRepository;
        private string _salt = "00209b47-08d7-475d-a0fb-20abf0872ba0";

        public UserRepository(JustTradeItDbContext dbContext, ITokenRepository tokenRepository)
        {
            _dbContext = dbContext;
            _tokenRepository = tokenRepository;
        }

        public UserDto AuthenticateUser(LoginInputModel loginInputModel)
        {
            var user = _dbContext.Users.FirstOrDefault(u =>
                u.Email == loginInputModel.Email &&
                u.HashedPassword == HashPassword(loginInputModel.Password));
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
            if (!(user == null)) { return null; }

            var nextId = _dbContext.Users.Max(table => table.Id) +1;
            var newIdentifier = Guid.NewGuid().ToString();

            _dbContext.Users.Add(new User{
                Id = nextId,
                PublicIdentifier = newIdentifier,
                FullName = inputModel.FullName,
                Email = inputModel.Email,
                ProfileImageUrl = "/someurl/",
                HashedPassword = HashPassword(inputModel.Password)
            });
            _dbContext.SaveChanges();

            var token = _tokenRepository.CreateNewToken();

            return new UserDto{
                Identifier = newIdentifier,
                FullName = inputModel.FullName,
                Email = inputModel.Email,
                TokenId = token.Id
            };
        }

        public UserDto GetProfileInformation(string email)
        {
            throw new NotImplementedException();
        }

        public UserDto GetUserInformation(string userIdentifier)
        {
            throw new NotImplementedException();
        }

        public void UpdateProfile(string email, string profileImageUrl, ProfileInputModel profile)
        {
            var newProfile = _dbContext.Users.FirstOrDefault(n => n.Email == email);

            newProfile.FullName = profile.FullName;
            newProfile.ProfileImageUrl = profile.ProfileImage.ToString();

            _dbContext.SaveChanges();
        }

        private string HashPassword(string password)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: CreateSalt(),
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 1000,
                numBytesRequested: 256 / 8
            ));
        }

        private byte[] CreateSalt()
        {
            return Convert.FromBase64String(Convert.ToBase64String(Encoding.UTF8.GetBytes(_salt)));
        }
    }
}