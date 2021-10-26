﻿using System.Linq;
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

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public IActionResult CreateUser()
        {
            //TODO: implement Registers a user within the application
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public IActionResult AuthenticateUser([FromBody] LoginInputModel login)
        {
            //TODO: implement Signs the user in by checking the credentials
            //provided and issuing a JWT token in return

            var user = _accountService.AuthenticateUser(login);
            if (user == null) { return Unauthorized(); }
            //TODO: Return valid JWT token
            return Ok();
        }


        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            //TODO: implement  Logs the user out by voiding the provided JWT
            //token using the id found within the claim

            //TODO: Retreive token id from claim and blacklist token

            int.TryParse(User.Claims.FirstOrDefault(c => c.Type == "tokenId").Value, out var tokenId);
            _accountService.Logout(tokenId);
            return NoContent();
        }


        [HttpGet]
        [Route("profile")]
        public IActionResult GetProfileInformation()
        {
            var claims = User.Claims.Select(c => new
            {
                Type = c.Type,
                Value = c.Value
            });
            //TODO: implement - Gets the profile information associated with the
            //authenticated user
            return Ok(claims);
        }


        [HttpPut]
        [Route("profile")]
        public IActionResult UpdateProfile()
        {
            //TODO: implement - - Updates the profile information associated with
            // the authenticated user
            return Ok();
        }


    }
}
