using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.ApiClient;
using OnlineStore.Contracts.Products;
using OnlineStore.MVC.Models;
using System.Diagnostics;

namespace OnlineStore.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOnlineStoreApiClient _apiClient;

        public HomeController(
            ILogger<HomeController> logger,
            IOnlineStoreApiClient apiClient)
        {
            _logger = logger;
            _apiClient = apiClient;
        }

        public async Task<IActionResult> Index(int pageNumber = 1, CancellationToken cancellation = default)
        {
            //await _apiClient.AddProductAsync(new ShortProductDto
            //{
            //    Id = 1,
            //    Name = "�����1",
            //    Description = "�������� ������ 1",
            //    Price = 100,
            //    StockQuantity = 1,
            //    ImageUrl = "https://cdn.shopify.com/s/files/1/0343/4368/2183/products/fender-electric-guitars-solid-body-fender-player-telecaster-daphne-blue-w-3-ply-mint-pickguard-0140217504-17220745461895_2000x.jpg"
            //}, cancellation: cancellation);


            var products = new ShortProductList
            {
                PageNumber = 1,
                TotalCount = 11,
                Products = new ShortProductDto[]
                {
                    new ShortProductDto
                    {
                        Id = 1,
                        Name = "�����1",
                        Description = "�������� ������ 1",
                        Price = 100,
                        ImageUrl = "https://cdn.shopify.com/s/files/1/0343/4368/2183/products/fender-electric-guitars-solid-body-fender-player-telecaster-daphne-blue-w-3-ply-mint-pickguard-0140217504-17220745461895_2000x.jpg"
                    },
                    new ShortProductDto
                    {
                        Id = 2,
                        Name = "�����2",
                        Description = "�������� ������ 2",
                        Price = 200,
                        ImageUrl = "https://cdn.shopify.com/s/files/1/0343/4368/2183/products/fender-electric-guitars-solid-body-fender-player-telecaster-daphne-blue-w-3-ply-mint-pickguard-0140217504-17220745461895_2000x.jpg"
                    },
                    new ShortProductDto
                    {
                        Id = 3,
                        Name = "�����3",
                        Description = "�������� ������ 3",
                        Price = 300,
                        ImageUrl = "https://cdn.shopify.com/s/files/1/0343/4368/2183/products/fender-electric-guitars-solid-body-fender-player-telecaster-daphne-blue-w-3-ply-mint-pickguard-0140217504-17220745461895_2000x.jpg"
                    },
                    new ShortProductDto
                    {
                        Id = 4,
                        Name = "�����4",
                        Description = "�������� ������ 4",
                        Price = 400,
                        ImageUrl = "https://cdn.shopify.com/s/files/1/0343/4368/2183/products/fender-electric-guitars-solid-body-fender-player-telecaster-daphne-blue-w-3-ply-mint-pickguard-0140217504-17220745461895_2000x.jpg"
                    },
                    new ShortProductDto
                    {
                        Id = 5,
                        Name = "�����1",
                        Description = "�������� ������ 5",
                        Price = 500,
                        ImageUrl = "https://cdn.shopify.com/s/files/1/0343/4368/2183/products/fender-electric-guitars-solid-body-fender-player-telecaster-daphne-blue-w-3-ply-mint-pickguard-0140217504-17220745461895_2000x.jpg"
                    },
                    new ShortProductDto
                    {
                        Id = 6,
                        Name = "�����6",
                        Description = "�������� ������ 6",
                        Price = 600,
                        ImageUrl = "https://cdn.shopify.com/s/files/1/0343/4368/2183/products/fender-electric-guitars-solid-body-fender-player-telecaster-daphne-blue-w-3-ply-mint-pickguard-0140217504-17220745461895_2000x.jpg"
                    },
                    new ShortProductDto
                    {
                        Id = 7,
                        Name = "�����7",
                        Description = "�������� ������ 7",
                        Price = 700,
                        ImageUrl = "https://cdn.shopify.com/s/files/1/0343/4368/2183/products/fender-electric-guitars-solid-body-fender-player-telecaster-daphne-blue-w-3-ply-mint-pickguard-0140217504-17220745461895_2000x.jpg"
                    },
                    new ShortProductDto
                    {
                        Id = 8,
                        Name = "�����8",
                        Description = "�������� ������ 8",
                        Price = 800,
                        ImageUrl = "https://cdn.shopify.com/s/files/1/0343/4368/2183/products/fender-electric-guitars-solid-body-fender-player-telecaster-daphne-blue-w-3-ply-mint-pickguard-0140217504-17220745461895_2000x.jpg"
                    },
                    new ShortProductDto
                    {
                        Id = 9,
                        Name = "�����9",
                        Description = "�������� ������ 9",
                        Price = 900,
                        ImageUrl = "https://cdn.shopify.com/s/files/1/0343/4368/2183/products/fender-electric-guitars-solid-body-fender-player-telecaster-daphne-blue-w-3-ply-mint-pickguard-0140217504-17220745461895_2000x.jpg"
                    },
                    new ShortProductDto
                    {
                        Id = 10,
                        Name = "�����10",
                        Description = "�������� ������ 10",
                        Price = 1000,
                        ImageUrl = "https://cdn.shopify.com/s/files/1/0343/4368/2183/products/fender-electric-guitars-solid-body-fender-player-telecaster-daphne-blue-w-3-ply-mint-pickguard-0140217504-17220745461895_2000x.jpg"
                    },
                    new ShortProductDto
                    {
                        Id = 11,
                        Name = "�����11",
                        Description = "�������� ������ 11",
                        Price = 1100,
                        ImageUrl = "https://cdn.shopify.com/s/files/1/0343/4368/2183/products/fender-electric-guitars-solid-body-fender-player-telecaster-daphne-blue-w-3-ply-mint-pickguard-0140217504-17220745461895_2000x.jpg"
                    },
                }
            };
            return View(products);
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
