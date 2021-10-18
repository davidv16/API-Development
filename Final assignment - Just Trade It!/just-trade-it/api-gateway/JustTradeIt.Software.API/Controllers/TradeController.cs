using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JustTradeIt.Software.API.Controllers
{
    [Authorize]
    [Route("api/trades")]
    [ApiController]
    public class TradeController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public IActionResult GetAllTrades()
        {
            return Ok();
        }

        [HttpPost]
        [Route("")]
        public IActionResult RequestTrade()
        {
            return Ok();
        }

        [HttpGet]
        [Route("{identifier: string}")]
        public IActionResult Login()
        {
            return Ok();
        }

        [HttpPut]
        [Route("{identifier: string}")]
        public IActionResult UpdateTradeRequestStatus()
        {
            return Ok();
        }
    }
}