using Microsoft.AspNetCore.Mvc;
using OnlineStore.AppServices.Products.Services;
using OnlineStore.Contracts.Products;
using OnlineStore.MVC.Attributes;

namespace OnlineStore.MVC.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    [JwtAuthorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(
            IProductService productService)
        {
            _productService = productService;
        }

        [Route("add/product")]
        [HttpPost]
        public async Task<IActionResult> AddProductAsync([FromBody] ShortProductDto productDto, CancellationToken cancellation)
        {
            await _productService.AddProductAsync(productDto, cancellation);

            return NoContent();
        }

        [Route("")]
        [HttpGet]
        public async Task<IActionResult> GetProductsAsync(CancellationToken cancellation)
        {
            var result = await _productService.GetProductsAsync(new Contracts.Common.PagedRequest
            {
                PageNumber = 1,
                PageSize = 10
            }, cancellation);

            return Ok(result);
        }
    }
}
