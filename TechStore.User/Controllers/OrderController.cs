using Microsoft.AspNetCore.Mvc;
using TechStore.User.Payload;
using TechStore.User.Services;

namespace TechStore.User.Controllers
{
    public class OrderController : Controller
    {
        private IOrderService _orderService;
        public OrderController(IOrderService userService)
        {
            _orderService = userService;
        }
        [HttpPost("/api/orders")]
        public IActionResult GetUserById([FromBody] CreateOrderRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _orderService.Create(request);
            return StatusCode(result.ResponseCode, result);

        }
    }
}
