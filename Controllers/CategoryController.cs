using BasetApi.Concrete;
using BasetApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasetApi.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]

    public class CategoryController : ControllerBase
    {
        private readonly Context _context;

        public CategoryController(Context context)
        {
            _context = context;
        }
         [HttpGet("GetAllCategory")]
        public IActionResult GetAllCategory()
        {
            var values = _context.Categories.ToList();
            return Ok(values);
        }

        [HttpGet("GetCategoryById/{id}")]
        public IActionResult GetById(int id)
        {
            var value = _context.Categories.Find(id);

            if (value == null)
            {
                return NotFound();
            }
            return Ok(value);
        }
        [HttpPost("AddnewCategory")]
        public IActionResult AddCategory([FromBody] Category category)
        {
            var value = _context.Categories.Add(category);
            _context.SaveChanges();
            return Ok("Category eklendi: " + value);
        }
        [HttpPut("UpdateCategoryById/{id}")]
        public IActionResult UpdateCategory(int id, [FromBody] Category category)
        {
            var value = _context.Categories.Find(id);
            if (value == null)
            {
                return NotFound();
            }
            category.CategoryName = value.CategoryName;
            category.CategoryStatus = value.CategoryStatus;

            _context.SaveChanges();
            return NoContent();

        }

        [HttpDelete("DeleteteCategoryById/{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var value = _context.Categories.Find(id);
            if (value == null)
            {
                return NotFound();
            }
            _context.Categories.Remove(value);
            _context.SaveChanges();
            
            return NoContent();
        }

        
    }
}