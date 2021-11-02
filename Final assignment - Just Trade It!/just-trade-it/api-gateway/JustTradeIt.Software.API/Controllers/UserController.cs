using System.Xml.Serialization;
using JustTradeIt.Software.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JustTradeIt.Software.API.Controllers
{
    [Authorize]
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        //TODO: implement Get a user profile Information
        [HttpGet]
        [Route("{identifier}")]
        public IActionResult GetUserInformation(string identifier)
        {

            return Ok(_userService.GetUserInformation(identifier));
        }

        //TODO: Get all successful trades associated with a user
        [HttpGet]
        [Route("{identifier}/trades")]
        public IActionResult GetUserTrades(string identifier)
        {
            return Ok(_userService.GetUserTrades(identifier));
        }

    }
}