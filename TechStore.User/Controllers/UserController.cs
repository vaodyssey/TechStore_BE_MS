using Microsoft.AspNetCore.Mvc;
using TechStore.User.Services;

namespace TechStore.User.Controllers
{
    public class UserController : Controller
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("/api/users/{userId}")]
        public IActionResult GetUserById([FromRoute] int userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _userService.GetUserById(userId);
            return StatusCode(result.ResponseCode, result);

        }
    }
}
