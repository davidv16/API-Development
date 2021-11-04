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
        public IActionResult GetItems([FromQuery] int pageNumber = 25, [FromQuery] int pageSize = 1, [FromQuery] bool ascendingSortOrder = true)
        {
            return Ok(_itemService.GetItems(pageSize, pageNumber, ascendingSortOrder));
        }

        [HttpGet]
        [Route("{identifier}", Name = "GetItemByIdentifier")]
        public IActionResult GetItemByIdentifier(string identifier)
        {
            var item = _itemService.GetItemByIdentifier(identifier);
            if (item == null) { return NotFound(); }
            
            return Ok(item);
        }

        [HttpPost]
        [Route("")]
        public IActionResult AddNewItem([FromBody] ItemInputModel item)
        {
            if (!ModelState.IsValid) { return StatusCode(412, item); };
            var identifier = _itemService.AddNewItem(User.Identity.Name, item);

            return CreatedAtRoute("GetItemByIdentifier", new { identifier }, null);
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