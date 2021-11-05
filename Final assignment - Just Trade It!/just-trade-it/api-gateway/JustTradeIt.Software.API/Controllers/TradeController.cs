using JustTradeIt.Software.API.Models.InputModels;
using JustTradeIt.Software.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JustTradeIt.Software.API.Controllers
{
    [Authorize]
    [Route("api/trades")]
    [ApiController]
    public class TradeController : ControllerBase
    {
        private readonly ITradeService _tradeService;

        public TradeController(ITradeService tradeService)
        {
            _tradeService = tradeService;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAllUserTrades([FromQuery] bool onlyIncludeActive)
        {
            //Gets all trades associated with the authenticated user
            return Ok(_tradeService.GetTradeRequests(User.Identity.Name, onlyIncludeActive));
        }

        [HttpPost]
        [Route("")]
        public IActionResult CreateTradeRequest(TradeInputModel tradeRequest)
        {
            //Requests a trade to a particular user. Trade proposal
            //always includes at least one item from each participant. Therefore if you want
            //to acquire a certain item, you must offer some of your items which you
            //believe are equally valuable as the desired item
            if (!ModelState.IsValid) { return StatusCode(412, tradeRequest); }

            var identifier = _tradeService.CreateTradeRequest(User.Identity.Name, tradeRequest);
            return CreatedAtRoute("GetTradeByIdentifier", new { identifier }, null);
        }

        [HttpGet]
        [Route("{identifier}", Name = "GetTradeByIdentifier")]
        public IActionResult GetTradeByIdentifier(string identifier)
        {
            //Get a detailed version of a trade request
            var trade = _tradeService.GetTradeByIdentifer(identifier);

            if (trade == null) { return NotFound(); }

            return Ok(trade);
        }

        [HttpPut]
        [Route("{identifier}")]
        public IActionResult UpdateTradeRequest(string identifier, [FromBody] string tradeStatus)
        {
            //Updates the status of a trade request. Only a
            //participant of the trade offering can update the status of the trade request.
            //Although if the trade request is in a finalized state it cannot be altered. The
            //only non finalized state is the pending state.
            if (!ModelState.IsValid) { return StatusCode(412, identifier); }

            _tradeService.UpdateTradeRequest(identifier, User.Identity.Name, tradeStatus);
            return NoContent();
        }
    }
}