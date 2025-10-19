using BasetApi.Concrete;
using BasetApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ProductController : ControllerBase
    {
        private readonly Context _context;

        public ProductController(Context context)
        {
            _context = context;
        }

        [HttpGet("GetAllProduct")]
        public IActionResult GetAllProducts()
        {
            var values = _context.Products.ToList();
            return Ok(values);
        }

        [HttpGet("GetProductById{id}")]
        public IActionResult GetByProduct(int id)
        {
            var value = _context.Products.Find(id);
            if (value == null)
            {
                return NotFound();
            }
            return Ok(value);
        }

        [HttpPost("AddNewProduct")]
        public IActionResult AddProduct([FromBody] Product product)
        {
            var value = _context.Products.Add(product);
            _context.SaveChanges();

            return Ok(value);
        }

        [HttpPut("ProductUpdateById{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] Product product)
        {
            var value = _context.Products.Find(id);
            if (value == null)
            {
                return NotFound();
            }

            product.ProductName = value.ProductName;
            product.ProductPrice = value.ProductPrice;

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("ProductDeleteById/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var value = _context.Products.Find(id);
            if (value == null)
            {
                return NotFound();
            }

            _context.Products.Remove(value);
            _context.SaveChanges();

            return NoContent();
        }

        
        



    }
}