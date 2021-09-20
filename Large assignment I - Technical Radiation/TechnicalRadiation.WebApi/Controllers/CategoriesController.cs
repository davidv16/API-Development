using TechnicalRadiation.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.WebApi.Attributes;

namespace TechnicalRadiation.WebApi.Controllers
{
    [Route("api/categories")]
    public class CategoriesController: Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        
        [HttpGet]
        [Route("")]
        public IActionResult GetAllCategories()
        {
            var categories = _categoryService.GetAllCategories();

            return Ok(categories);
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetCategoriesById")]
        public IActionResult GetCategoriesById(int id)
        {
            var category = _categoryService.GetCategoryById(id);

            return Ok(category);
        }


        [AuthorizationAttribute]
        [HttpPost]
        [Route("{categoryId:int}/newsItems/{newsItemId:int}", Name = "LinkCategoriesNewsItem")]
        public IActionResult LinkCategoryByCategoryIdAndNewsItemId(int categoryId, int newsItemId)
        {
            if(_categoryService.LinkCategoryByCategoryIdAndNewsItemId(categoryId, newsItemId) == false)
            {
                return NotFound();
            }
            _categoryService.LinkCategoryByCategoryIdAndNewsItemId(categoryId, newsItemId);

            return StatusCode(201);
        }

        [AuthorizationAttribute]
        [HttpPost]
        [Route("")]
        public IActionResult CreateCategory([FromBody] CategoryInputModel category)
        {
            if (!ModelState.IsValid) { return StatusCode(412, category); };

            var id = _categoryService.CreateCategory(category);
            return CreatedAtRoute("GetCategoriesById", new { id }, null);
        }

        [AuthorizationAttribute]
        [HttpPut("{id:int}")]
        public IActionResult UpdateCategory(int id, [FromBody] CategoryInputModel categoryData)
        {
            var itemExists = _categoryService.GetCategoryById(id);
            if(itemExists == null) { return NotFound(); }
            
            if (!ModelState.IsValid) { return StatusCode(412, categoryData); };

            _categoryService.UpdateCategory(id, categoryData);
            return StatusCode(201);
        }

        [AuthorizationAttribute]
        [HttpDelete("{id:int}")]
        public IActionResult DeleteCategory(int id)
        {
            var itemExists = _categoryService.GetCategoryById(id);
            if(itemExists == null) { return NotFound(); }

            _categoryService.DeleteCategory(id);
            return NoContent();
        }
        
    
    }
}