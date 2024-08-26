
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Globalization;
using TechStore.Products.Payload;
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
        [HttpGet("/api/products")]
        public IActionResult Register(GetByRequest request)
        {
            var result = _productService.GetBy(request);
            return StatusCode(result.ResponseCode, result);

        }
        [HttpGet("/api/products/{id}")]
        public IActionResult Register([FromRoute] int id = 0)
        {
            var result = _productService.GetById(id);
            return StatusCode(result.ResponseCode, result);

        }
    }

}
