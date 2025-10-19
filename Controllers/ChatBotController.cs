using System.Threading.Tasks;
using BasetApi.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BasetApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class ChatBotController : Controller
    {
        private readonly Context _context;

        public ChatBotController(Context context)
        {
            _context = context;
        }

        [HttpPost("chatbot")]
        public async Task<IActionResult> GetProductByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Mehsul Adı Girilmedi.");
            }

            var product = await _context.Products
                .Where(x => x.ProductName != null &&
                            x.ProductName.ToLower().Contains(name.ToLower()))
                .FirstOrDefaultAsync();

            if (product == null)
            {
                return NotFound($"Axtarılan Mehsul '{name}' mövcud deyil.");
            }

            return Ok(product);
        }

    }
}