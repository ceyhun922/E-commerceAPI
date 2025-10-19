using System.Threading.Tasks;
using BasetApi.Concrete;
using BasetApi.Models;
using BasetApi.Models.Dto;
using BasetApi.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace BasetApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class RegisterController : ControllerBase
    {
        private readonly Context _context;
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;
        private readonly JwtService _jwtService;

        public RegisterController(Context context, IConfiguration config, UserManager<User> userManager, JwtService jwtService)
        {
            _context = context;
            _config = config;
            _userManager = userManager;
            _jwtService = jwtService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var exitingEmail = await _userManager.FindByEmailAsync(dto.EMail);

            if (exitingEmail != null)
            {
                return BadRequest("Bu Email İstifadedir");
            }

            var user = new User
            {
                UserName = dto.EMail.Split('@')[0],
                Name = dto.Name,
                Surname = dto.SurName,
                Email = dto.EMail,
                PhoneNumber = dto.Number
            };

            var result = await _userManager.CreateAsync(user, dto.UserPassword);

            if (!result.Succeeded)
            {
                var error = result.Errors.Select(e => e.Description);
                return BadRequest(new { Error = error });
            }

            await _userManager.AddToRoleAsync(user, "User");

            var token = await _jwtService.GenerateToken(user);

             return Ok(new
                {
                    message = "Qeydiyyat Uğurludur",
                    token
                });
        }


        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgetPasswordDto dto)
        {
            var user = _context.Users.FirstOrDefault();
            if (user == null) return BadRequest("Böyle bir kullanıcı yok.");

            var resetToken = Guid.NewGuid().ToString();
            user.ResetToken = resetToken;
            user.ResetTokenExpire = DateTime.UtcNow.AddMinutes(30);
            _context.SaveChanges();

            var client = new SendGridClient(_config["SendGrid:ApiKey"]);
            var from = new EmailAddress(_config["SendGrid:FromEmail"], _config["SendGrid:FromName"]);
            var subject = "Şifre Sıfırlama";
            var to = new EmailAddress();
            var resetLink = $"http://localhost:3000/reset-password?token={resetToken}";
            var plainTextContent = $"Şifrenizi sıfırlamak için linke tıklayın: {resetLink}";
            var htmlContent = $"<strong>Şifrenizi sıfırlamak için <a href='{resetLink}'>tıklayın</a></strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            await client.SendEmailAsync(msg);

            return Ok("Şifre sıfırlama linki mail adresinize gönderildi.");
        }

        [HttpPost("ResetPassword")]
        public IActionResult ResetPassword([FromBody] ResetPasswordDto dto)
        {
            var user = _context.Users.FirstOrDefault(u => u.ResetToken == dto.Token);
            if (user == null || user.ResetTokenExpire < DateTime.UtcNow)
                return BadRequest("Token geçersiz veya süresi dolmuş.");

            if (dto.NewPassword != dto.NewPasswordConfirm)
                return BadRequest("Şifreler uyuşmuyor!");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            user.ResetToken = null;
            user.ResetTokenExpire = null;
            _context.SaveChanges();

            return Ok("Şifreniz başarıyla değiştirildi.");
        }


    }
}














