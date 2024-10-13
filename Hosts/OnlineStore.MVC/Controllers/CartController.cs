using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.AppServices.Carts.Services;

namespace OnlineStore.MVC.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int id, CancellationToken cancellation)
        {
            await _cartService.AddProductToCartAsync(id, cancellation);
            var cartCount = await _cartService.GetCartItemCountAsync(cancellation);
            return Json(new { success = true, cartCount });
        }

        [HttpGet]
        public async Task<IActionResult> GetCartItemCount(CancellationToken cancellation)
        {
            var cartItemCount = await _cartService.GetCartItemCountAsync(cancellation);

            return Json(new { cartItemCount });
        }
    }
}
