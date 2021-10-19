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
        public IActionResult GetTrades()
        {
            //TODO: implement Gets all trades associated with the authenticated user
            return Ok();
        }

        [HttpPost]
        [Route("")]
        public IActionResult GetTradeRequests()
        {
            //TODO: implement Requests a trade to a particular user. Trade proposal
            //always includes at least one item from each participant. Therefore if you want
            //to acquire a certain item, you must offer some of your items which you
            //believe are equally valuable as the desired item
            return Ok();
        }

        [HttpGet]
        [Route("{identifier: string}")]
        public IActionResult GetTradeByIdentifier()
        {
            //TODO: implement Get a detailed version of a trade request
            return Ok();
        }

        [HttpPut]
        [Route("{identifier: string}")]
        public IActionResult UpdateTradeRequest()
        {
            //TODO: implement Updates the status of a trade request. Only a
            //participant of the trade offering can update the status of the trade request.
            //Although if the trade request is in a finalized state it cannot be altered. The
            //only non finalized state is the pending state.
            return Ok();
        }
    }
}