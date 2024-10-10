using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineStore.ApiClient;
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
        private readonly IOnlineStoreApiClient _apiClient;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public HomeController(
            ILogger<HomeController> logger,
            IOnlineStoreApiClient apiClient,
            IProductService productService,
            ICategoryService categoryService)
        {
            _logger = logger;
            _apiClient = apiClient;
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
            
            return View(result);
        }

        public async Task<IActionResult> AddProduct(CancellationToken cancellation)
        {
            var categories = await _categoryService.GetCategoriesAsync(cancellation);

            ViewBag.Categories = new SelectList(categories, "CategoryId", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ShortProductDto productDto, IFormFile imageFile, CancellationToken cancellation)
        {
            await _productService.AddProductAsync(productDto, imageFile, cancellation);

            return Ok();
        }

        //public Task<IActionResult> Details(int id)
        //{
        //    var product = _productService.GetProductById(id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(product);
        //}

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
