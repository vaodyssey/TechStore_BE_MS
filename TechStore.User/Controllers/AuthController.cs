
using Microsoft.AspNetCore.Mvc;
using TechStore.User.Payload;
using TechStore.User.Services;

namespace TechStore.User.Controllers
{

    [ApiController]
    public class AuthController : Controller
    {
        private IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("/api/auth/register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _authService.Register(request);
            return StatusCode(result.ResponseCode, result);

        }
        [HttpPost("/api/auth/login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _authService.Login(request);
            return StatusCode(result.ResponseCode, result);

        }
        [HttpPost("/auth/validate")]
        public IActionResult ValidateToken([FromHeader] string token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _authService.ValidateToken(token);
            return StatusCode(result.ResponseCode, result);

        }
    }

}
