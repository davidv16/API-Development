using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JustTradeIt.Software.API.Controllers
{
    [Authorize]
    [Route("api/items")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public IActionResult GetItems()
        {
            //TODO: implement Get all available items
            //The result is an envelope containing the results in pages.
            return Ok();
        }

        [HttpGet]
        [Route("{identifier:string}")]
        public IActionResult GetItemByIdentifier()
        {
            //TODO: implement Gets a detailed version of an item by identifier
            return Ok();
        }

        [HttpPost]
        [Route("")]
        public IActionResult AddNewItem()
        {
            //TODO: implement Create a new item which will be associated with the
            //authenticated user and other users will see the new item and can request a
            //trade to acquire that item
            return Ok();
        }
        [HttpDelete]
        [Route("{identifier:string}")]
        public IActionResult RemoveItem()
        {
            //TODO: Implement - Delete an item from the inventory of the
            //authenticated user. The item should only be soft deleted from the database.
            //All trade requests which include the deleted item should be marked as
            //cancelled
            return Ok();
        }
    }
}