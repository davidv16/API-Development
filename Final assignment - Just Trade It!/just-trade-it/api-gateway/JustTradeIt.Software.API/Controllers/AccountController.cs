using System;
using System.Linq;
using JustTradeIt.Software.API.Models.InputModels;
using JustTradeIt.Software.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JustTradeIt.Software.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;

        public AccountController(IAccountService accountService, ITokenService tokenService)
        {
            _accountService = accountService;
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public IActionResult CreateUser([FromBody] RegisterInputModel user)
        {
            if (!ModelState.IsValid) { return StatusCode(412, user); };
            var newUser = _accountService.CreateUser(user);

            return CreatedAtRoute("GetUserInformation", new { newUser.Identifier }, null);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public IActionResult AuthenticateUser([FromBody] LoginInputModel login)
        {
            var user = _accountService.AuthenticateUser(login);
            if (user == null) { return Unauthorized(); }
            var token = _tokenService.GenerateJwtToken(user);
            return Ok(token);
        }


        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            int.TryParse(User.Claims.FirstOrDefault(c => c.Type == "tokenId").Value, out var tokenId);
            _accountService.Logout(tokenId);
            return NoContent();
        }


        [HttpGet]
        [Route("profile")]
        public IActionResult GetProfileInformation()
        {
            var name = User.Claims.FirstOrDefault(u => u.Type == "name").Value;
            var profile = _accountService.GetProfileInformation(name);
            return Ok(profile);
        }


        [HttpPut]
        [Route("profile")]
        public IActionResult UpdateProfile([FromForm] ProfileInputModel profile)
        {
            if (!ModelState.IsValid) { return StatusCode(412, profile); };

            _accountService.UpdateProfile(User.Identity.Name, profile);
            return NoContent();
        }


    }
}
