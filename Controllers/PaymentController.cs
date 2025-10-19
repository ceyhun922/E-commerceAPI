using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasetApi.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        [HttpPost("Payment")]
        public IActionResult Index()
        {
            var values = "Burada Ödeniş Olacaq";
            return Ok(values);
        }

    }
}