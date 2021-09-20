using TechnicalRadiation.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.WebApi.Attributes;

namespace TechnicalRadiation.WebApi.Controllers
{
    [Route("api/authors")]
    public class AuthorsController : Controller
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }
        [HttpGet]
        [Route("")]
        public IActionResult GetAllAuthors()
        {
            return Ok(_authorService.GetAllAuthors());
        }

        
        [HttpGet]
        [Route("{id:int}", Name = "GetAuthorsById")]
        public IActionResult GetAuthorById(int id)
        {
            return Ok(_authorService.GetAuthorById(id));
        }

        // Get's all news items by Author
        [HttpGet]
        [Route("{id:int}/newsItems", Name = "GetAuthorsNewsItems")]
        public IActionResult GetNewsItemsByAuthorId(int id)
        {
            return Ok(_authorService.GetAllNewsItemsByAuthorId(id));
        }
 
        [AuthorizationAttribute]
        [HttpPost]
        [Route("{authorId:int}/newsItems/{newsItemId:int}", Name = "LinkAuthorsNewsItem")]
        public IActionResult LinkNewsItemByAuthorIdAndNewsItemId(int authorId, int newsItemId)
        {
            if(_authorService.LinkNewsItemByAuthorIdAndNewsItemId(authorId, newsItemId) == false)
            {
                return NotFound();
            }
            _authorService.LinkNewsItemByAuthorIdAndNewsItemId(authorId, newsItemId);

            return StatusCode(201);
        }

        [AuthorizationAttribute]
        [HttpPost]
        [Route("")]
        public IActionResult CreateAuthor([FromBody] AuthorInputModel author)
        {
            if (!ModelState.IsValid) { return StatusCode(412, author); };

            var id = _authorService.CreateAuthor(author);
            return CreatedAtRoute("GetAuthorsNewsItems", new { id }, null);
        }

        [AuthorizationAttribute]
        [HttpPut("{id:int}")]
        public IActionResult UpdateAuthor(int id, [FromBody] AuthorInputModel authorData)
        {
            var itemExists = _authorService.GetAuthorById(id);
            if(itemExists == null) { return NotFound(); }

            if (!ModelState.IsValid) { return StatusCode(412, authorData); };

            _authorService.UpdateAuthor(id, authorData);
            return StatusCode(201);
        }

        [AuthorizationAttribute]
        [HttpDelete("{id:int}")]
        public IActionResult DeleteAuthor(int id)
        {
            var itemExists = _authorService.GetAuthorById(id);
            if(itemExists == null) { return NotFound(); }

            _authorService.DeleteAuthor(id);
            return NoContent();
        }        
    }
}