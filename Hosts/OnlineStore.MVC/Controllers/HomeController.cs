using Microsoft.AspNetCore.Mvc;
using OnlineStore.AppServices.Attributes.Services;
using OnlineStore.AppServices.Products.Services;
using OnlineStore.Domain.Entities;
using OnlineStore.MVC.Models;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace OnlineStore.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductAttributeService _productAttributeService;
        private readonly IProductService _productService;

        public HomeController(
            ILogger<HomeController> logger,
            IProductAttributeService productAttributeService,
            IProductService productService)
        {
            _logger = logger;
            _productAttributeService = productAttributeService;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            //var entity = await _productAttributeService.GetAsync(1);
            var products = await _productService.GetProductsAsync();
            return View();
        }

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
