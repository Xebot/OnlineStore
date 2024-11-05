using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineStore.AppServices.Categories.Services;
using OnlineStore.AppServices.Products.Services;
using OnlineStore.Contracts.Common;
using OnlineStore.Contracts.Products;
using OnlineStore.MVC.Models;
using System.Diagnostics;

namespace OnlineStore.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public HomeController(
            ILogger<HomeController> logger,
            IProductService productService,
            ICategoryService categoryService)
        {
            _logger = logger;
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(int pageNumber = 1, CancellationToken cancellation = default)
        {
            var result = await _productService.GetProductsAsync(new PagedRequest
            {
                PageNumber = pageNumber,
                PageSize = 6
            }, cancellation);

            _logger.LogInformation("Получено продуктов: {Count}, Страница: {PageNumber}, Всего продуктов: {TotalCount}", result.Result.Count, result.PageNumber, result.TotalCount);

            return View(result);
        }

        public async Task<IActionResult> AddProduct(CancellationToken cancellation)
        {
            var categories = await _categoryService.GetCategoriesAsync(cancellation);

            ViewBag.Categories = new SelectList(categories, "CategoryId", "Name");
            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ShortProductDto productDto, CancellationToken cancellation)
        {
            await _productService.AddProductAsync(productDto, cancellation);

            return Ok();
        }

        public async Task<IActionResult> Details(int id, CancellationToken cancellation)
        {
            var product = await _productService.GetProductByIdAsync(id, cancellation);
            if (product == null)
            {
                return NotFound();
            }

            return View("ProductDetails", product);
        }

        [Authorize(Roles ="User")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
