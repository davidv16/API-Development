using JustTradeIt.Software.API.Models.InputModels;
using JustTradeIt.Software.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JustTradeIt.Software.API.Controllers
{
    [Authorize]
    [Route("api/items")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetItems([FromQuery] int pageNumber, [FromQuery] int pageSize, [FromQuery] bool ascendingSortOrder)
        {
            //TODO: implement Get all available items
            //The result is an envelope containing the results in pages.


            return Ok(_itemService.GetItems(pageSize, pageNumber, ascendingSortOrder));
        }

        [HttpGet]
        [Route("{identifier}")]
        public IActionResult GetItemByIdentifier(string identifier)
        {
            //TODO: implement Gets a detailed version of an item by identifier
            return Ok(_itemService.GetItemByIdentifier(identifier));
        }

        [HttpPost]
        [Route("")]
        public IActionResult AddNewItem([FromBody] ItemInputModel item)
        {
            //TODO: implement Create a new item which will be associated with the
            //authenticated user and other users will see the new item and can request a
            //trade to acquire that item
            return Ok(_itemService.AddNewItem(User.Identity.Name, item));
        }
        [HttpDelete]
        [Route("{identifier}")]
        public IActionResult RemoveItem(string identifier)
        {
            //TODO: Implement - Delete an item from the inventory of the
            //authenticated user. The item should only be soft deleted from the database.
            //All trade requests which include the deleted item should be marked as
            //cancelled
            _itemService.RemoveItem(User.Identity.Name, identifier);
            return NoContent();
        }
    }
}