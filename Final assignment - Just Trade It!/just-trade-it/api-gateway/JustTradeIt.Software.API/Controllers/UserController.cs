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

        [HttpGet]
        [Route("{identifier}", Name = "GetUserInformation")]
        public IActionResult GetUserInformation(string identifier)
        {
            var userInfo = _userService.GetUserInformation(identifier);
            if (userInfo == null) { return NotFound(); }

            return Ok(userInfo);
        }

        [HttpGet]
        [Route("{identifier}/trades")]
        public IActionResult GetUserTrades(string identifier)
        {
            var userTrades = _userService.GetUserTrades(identifier);
            if (userTrades == null) { return NotFound(); }

            return Ok(userTrades);
        }

    }
}