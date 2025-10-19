using System.Threading.Tasks;
using BasetApi.Concrete;
using BasetApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BasetApi.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly Context _context;

        public UserController(Context context)
        {
            _context = context;
        }
        //Users
        [HttpGet("GetAllUser")]
        public IActionResult GetAllUser()
        {
            var values = _context.Users.ToList();

            return Ok(values);
        }

        [HttpGet("GetUserById/{id}")]
        public IActionResult GetByUser(string id)
        {
            var value = _context.Users.Find(id);

            if (value == null)
            {
                return NotFound();
            }

            return Ok(value);
        }

        [HttpPut("UserUpdateById/{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] User user)
        {
            var value = await _context.Users.FindAsync(id);
            if (value == null) return NotFound();

            value.Name = user.Name;
            value.Surname = user.Surname;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("UserDeleteById/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var value = await _context.Users.FindAsync(id);

            if (value == null) return NotFound();

            _context.Remove(value);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateUserAsync(string id, [FromBody] User user)
        {
            var value = await _context.Users.FindAsync(id);
            if (value == null) return NotFound();

            value.Name = user.Name;
            value.Surname = user.Surname;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [Authorize]
        [HttpPut("ChangePassword/{id}")]
        public IActionResult ChangePassword(string id, [FromBody] ChangePasswordDto dto)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();

            if (!BCrypt.Net.BCrypt.Verify(dto.CurrentPassword, user.PasswordHash))
            {
                return BadRequest("Mevcut şifre yanlış.");
            }

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);

            _context.SaveChanges();
            return Ok("Deyişdi");
        }

        [HttpGet("MyOrder/{userID}")]
        public async Task<IActionResult> MyOrder(string userID)        {
            var values = await _context.Orders
            .Where(x => x.UserId == userID)
            .Include(x => x.OrderItems)
            .ThenInclude(x => x.Product)
            .ToListAsync();
            return Ok(values);
        }







    }
}