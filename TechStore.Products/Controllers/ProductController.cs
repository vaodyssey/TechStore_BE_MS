
using Microsoft.AspNetCore.Mvc;
using TechStore.Products.Services;


namespace TechStore.Products.Controllers
{

    [ApiController]
    public class ProductController : Controller
    {
        private IProductService _productService;
        public ProductController(IProductService authService)
        {
            _productService = authService;
        }
        [HttpGet("/products")]
        public IActionResult Register(
            [FromQuery] string searchTerm = null,
            [FromQuery] string label = null,
            [FromQuery] int minPrice = 0,
            [FromQuery] int maxPrice = 0,
            [FromQuery] string sortBy = null,
            [FromQuery] string sortOrder = null,
            [FromQuery] int pageNumber = 0,
            [FromQuery] int pageSize = 0

            )
        {
            var result = _productService.GetBy(new Payload.GetByRequest
            {
                SearchTerm = searchTerm,
                Label = label,
                MaxPrice = maxPrice,
                MinPrice = minPrice,
                PageNumber = pageNumber,
                PageSize = pageSize,
                SortBy = sortBy,
                SortOrder = sortOrder
            }
            );
            return StatusCode(result.ResponseCode, result);

        }
        [HttpGet("/products/{id}")]
        public IActionResult Register([FromRoute] int id = 0)
        {
            var result = _productService.GetById(id);
            return StatusCode(result.ResponseCode, result);

        }
    }

}
