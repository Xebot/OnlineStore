using Microsoft.AspNetCore.Mvc;
using OnlineStore.AppServices.Attributes.Services;
using OnlineStore.Domain.Entities;
using OnlineStore.MVC.Models;
using System.Diagnostics;

namespace OnlineStore.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductAttributeService _productAttributeService;

        public HomeController(
            ILogger<HomeController> logger,
            IProductAttributeService productAttributeService)
        {
            _logger = logger;
            _productAttributeService = productAttributeService;
        }

        public async Task<IActionResult> Index()
        {
            var entity = await _productAttributeService.GetAsync(1);
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
