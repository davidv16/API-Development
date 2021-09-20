using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using TechnicalRadiation.WebApi.Attributes;
using TechnicalRadiation.Models;
using TechnicalRadiation.Models.Dtos;

namespace TechnicalRadiation.WebApi.Controllers
{
    [Route("api")]
    public class NewsItemController : Controller
    {
        private readonly INewsItemService _newsItemService;

        public NewsItemController(INewsItemService newsItemService)
        {
            _newsItemService = newsItemService;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAllNewsItems([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 25)
        {
            var newsItems = _newsItemService.GetAllNewsItems();

            var envelope = new Envelope<NewsItemDto>(pageNumber, pageSize, newsItems);

            return Ok(envelope);
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetNewsItemsById")]
        public IActionResult GetNewsItemById(int id) {
            var newsItem = _newsItemService.GetNewsItemById(id);

            if (newsItem == null)
            {
                return NotFound();
            }

            return Ok(newsItem);
        }
        
        [AuthorizationAttribute]
        [HttpPost]
        [Route("")]
        public IActionResult CreateNewsItem([FromBody] NewsItemInputModel newsItem)
        {
            if (!ModelState.IsValid) { return StatusCode(412, newsItem); };

            var id = _newsItemService.CreateNewsItem(newsItem);
            return CreatedAtRoute("GetNewsItemsById", new { id }, null);
        }

        [AuthorizationAttribute]
        [HttpPut("{id:int}")]
        public IActionResult UpdateNewsItem(int id, [FromBody] NewsItemInputModel newsItemData)
        {
            var itemExists = _newsItemService.GetNewsItemById(id);
            if(itemExists == null) { return NotFound(); }
            
            if (!ModelState.IsValid) { return StatusCode(412, newsItemData); };
            
            _newsItemService.UpdateNewsItem(id, newsItemData);
            return StatusCode(201);
        }

        [AuthorizationAttribute]
        [HttpDelete("{id:int}")]
        public IActionResult DeleteNewsItem(int id)
        {
            var itemExists = _newsItemService.GetNewsItemById(id);
            if(itemExists == null) { return NotFound(); }
            _newsItemService.DeleteNewsItem(id);
            return NoContent();
        }
        
    }
}