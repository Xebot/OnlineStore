using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.ApiClient;
using OnlineStore.AppServices.Products.Services;
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

        public HomeController(
            ILogger<HomeController> logger,
            IOnlineStoreApiClient apiClient,
            IProductService productService)
        {
            _logger = logger;
            _apiClient = apiClient;
            _productService = productService;
        }

        public async Task<IActionResult> Index(int pageNumber = 1, CancellationToken cancellation = default)
        {
            
            var res = _productService.GetProductsAsync(cancellation);
            


            
            return View(res);
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
