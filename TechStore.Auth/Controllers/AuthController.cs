
using Microsoft.AspNetCore.Mvc; 
using TechStore.Auth;
using TechStore.Auth.Services;

namespace TechStore.Auth.Controllers
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
        public IActionResult Register([FromBody] Payload.RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _authService.Register(request);
            return StatusCode(result.ResponseCode, result);

        }
        [HttpPost("/api/auth/login")]
        public IActionResult Login([FromBody] Payload.LoginRequest request)
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
