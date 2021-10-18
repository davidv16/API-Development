using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JustTradeIt.Software.API.Controllers
{
    [Authorize]
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        [Route("{identifier: string}")]
        public IActionResult GetUserProfileInfo()
        {
            return Ok();
        }

        [HttpGet]
        [Route("{identifier: string}/trades")]
        public IActionResult GetAllSuccessfulTrades()
        {
            return Ok();
        }

    }
}