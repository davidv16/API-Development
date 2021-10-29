using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JustTradeIt.Software.API.Controllers
{
    [Authorize]
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //TODO: implement Get a user profile Information
        [HttpGet]
        [Route("{identifier:string}")]
        public IActionResult GetUserInformation(string identifier)
        {
            return Ok();
        }

        //TODO: Get all successful trades associated with a user
        [HttpGet]
        [Route("{identifier:string}/trades")]
        public IActionResult GetUserTrades(string identifier)
        {
            return Ok();
        }

    }
}