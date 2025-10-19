
using System.Threading.Tasks;
using BasetApi.Concrete;
using BasetApi.Models;
using BasetApi.Models.Dto;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BasetApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]

    public class AuthController : ControllerBase
    {
        private readonly Context _context;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _config;

        public AuthController(Context context, IConfiguration config, SignInManager<User> signInManager)
        {
            _context = context;
            _config = config;
            _signInManager = signInManager;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = _context.Users.SingleOrDefault(u => u.Email == dto.UserMail);

            if (user == null)
            {
                return Unauthorized("Şifre veya İstifadeçi adı sehvdir");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);

            if (!result.Succeeded)
            {
                return Unauthorized("Şifre veya İstifadeçi adı sehvdir");
            }

            return Ok(new
            {
                Message = "Giriş Uğurludur",
            });
        }

        /* 
                [HttpPost("GoogleAuth")]
                public async Task<IActionResult> GoogleAuth(GoogleloginDto dto)
                {
                    GoogleJsonWebSignature.Payload payload;
                    try
                    {
                        payload = await GoogleJsonWebSignature.ValidateAsync(dto.IdToken);
                    }
                    catch (Exception)
                    {
                        return BadRequest("Google Hesabı Doğrulanmadı");
                    }

                    var email = payload.Email;
                    var name = payload.Name;
                    var googleId = payload.Subject;

                    var user = _context.Users.FirstOrDefault(x => x.UserMail == email);

                    if (user == null)
                    {
                        user = new User
                        {
                            UserMail = email,
                            Name = name,
                            GoogleId = googleId
                        };
                        _context.Users.FirstOrDefault(x => x.UserMail == email);
                        await _context.SaveChangesAsync();

                    }
                    var token = _jwtService.GenerateToken(user);
                    return Ok(new
                    {
                        token,
                        user = new
                        {
                            user.UserID,
                            user.Name,
                            user.UserMail
                        }
                    });
                }
         */

        [HttpPost("Logout")]

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok("Çıxış Etdiniz");
        }



    }


} 