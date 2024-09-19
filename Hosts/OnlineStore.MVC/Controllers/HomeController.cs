using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.AppServices.Asparagus;
using OnlineStore.MVC.Models;
using System.Diagnostics;

namespace OnlineStore.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAsparagusService _asparagusService;

        public HomeController(
            ILogger<HomeController> logger,
            IAsparagusService asparagusService)
        {
            _logger = logger;
            _asparagusService = asparagusService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
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

        [HttpGet]
        public async Task<IActionResult> Asparagus()
        {            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Asparagus(string email, string name)
        {
            await _asparagusService.ProcessAsparagusLoverAsync(email, name);
            return RedirectToAction("AsparagusList");
        }

        [HttpGet]
        public async Task<IActionResult> AsparagusList()
        {
            var list = await _asparagusService.GetList();
            return View(list);
        }
    }
}
