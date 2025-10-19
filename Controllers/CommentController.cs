using BasetApi.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasetApi.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly Context _context;

        public CommentController(Context context)
        {
            _context = context;
        }

        [HttpGet("GetAllComments")]
        public IActionResult GetAllComment()
        {
            var values = _context.Comments.ToList();
            return Ok(values);
        }
    }
}