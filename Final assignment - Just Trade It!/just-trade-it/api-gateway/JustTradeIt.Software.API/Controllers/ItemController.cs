using Microsoft.AspNetCore.Mvc;

namespace JustTradeIt.Software.API.Controllers
{
    [Route("api/items")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public IActionResult GetAllAvailableItems()
        {
            return Ok();
        }


        [HttpGet]
        [Route("{identifier: string}")]
        public IActionResult GetItemById()
        {
            return Ok();
        }

        [HttpPost]
        [Route("")]
        public IActionResult CreateItem()
        {
            return Ok();
        }
        [HttpDelete]
        [Route("{identifier: string}")]
        public IActionResult DeleteItem()
        {
            return Ok();
        }
    }
}