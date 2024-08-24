using Microsoft.AspNetCore.Mvc;
using TechStore.Products.Services;
using TechStore.Products.Services.Impl;

namespace TechStore.Products.Controllers
{
    public class BrandsController : Controller
    {
        private IBrandService _brandService;
        public BrandsController(IBrandService brandService) {
            _brandService = brandService;
        }

        [HttpGet("/api/brands")]
        public IActionResult GetAll()
        {
            var result = _brandService.GetAll();
            return StatusCode(result.ResponseCode, result);

        }
    }
}
