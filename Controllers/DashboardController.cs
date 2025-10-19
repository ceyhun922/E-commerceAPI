using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

//UI API
namespace BasetApi.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetDashboard() => Ok(new { stats = "ok" });
    }
}