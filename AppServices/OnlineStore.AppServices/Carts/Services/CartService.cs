using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using OnlineStore.AppServices.Carts.Repositories;
using OnlineStore.AppServices.Common.DateTimeProviders;
using OnlineStore.AppServices.Products.Services;
using OnlineStore.Contracts.Cart;
using OnlineStore.Contracts.Enums;
using OnlineStore.Domain.Entities;

namespace OnlineStore.AppServices.Carts.Services
{
    /// <summary>
    /// Сервис по работе с корзиной.
    /// </summary>
    public sealed class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IProductService _productService;

        public CartService(ICartRepository cartRepository,
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor,
            IDateTimeProvider dateTimeProvider,
            IProductService productService)
        {
            _cartRepository = cartRepository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _dateTimeProvider = dateTimeProvider;
            _productService = productService;
        }


        public async Task AddProductToCartAsync(int productId, CancellationToken cancellation)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var existingCart = await _cartRepository.GetCartByUserAsync(user.Id, cancellation);

            if (existingCart == null)
            {
                existingCart = new Cart
                {
                    UserId = user.Id,
                    Created = _dateTimeProvider.UtcNow,
                    StatusId = (int)CartStatusEnum.New
                };

                AddProductToCart(existingCart, productId);

                await _cartRepository.AddAsync(existingCart, cancellation);
            }
            else
            {
                existingCart.Updated = _dateTimeProvider.UtcNow;
                AddProductToCart(existingCart, productId);
                await _cartRepository.UpdateAsync(existingCart, cancellation);
            }
        }        

        public async Task<int?> GetCartItemCountAsync(CancellationToken cancellation)
        {
            var existingCart = await GetCurrentUserCartAsync(cancellation);

            return existingCart?.Products?.Sum(x => x.Quantity) ?? 0;
        }

        private static void AddProductToCart(Cart cart, int productId)
        {
            var productInCart = cart.Products.FirstOrDefault(p => p.ProductId == productId);

            if (productInCart != null)
            {
                productInCart.Quantity++;
            }
            else
            {
                cart.Products.Add(new CartProduct
                {
                    Cart = cart,
                    Quantity = 1,
                    ProductId = productId
                });
            }
        }

        /// <inheritdoc/>
        public async Task<CartDto> GetCartAsync(CancellationToken cancellation)
        {
            var cart = await GetCurrentUserCartAsync(cancellation);

            if (cart == null)
            {
                return new CartDto
                {
                    Items = [],
                    TotalAmount = 0
                };
            }

            return await GetCartItemsAsync(cart, cancellation);
        }

        public async Task RemoveItemAsync(int productId, CancellationToken cancellation)
        {
            var cart = await GetCurrentUserCartAsync(cancellation) 
                ?? throw new InvalidOperationException("Не найдена корзина текущего пользователя");

            var productInCart = cart.Products.FirstOrDefault(cp => cp.ProductId == productId)
                ?? throw new InvalidOperationException("Не найден товар в корзине текущего пользователя для удаления");

            cart.Products.Remove(productInCart);

            await _cartRepository.UpdateAsync(cart, cancellation);
        }

        private async Task<Cart> GetCurrentUserCartAsync(CancellationToken cancellation)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            if (user == null)
            {
                return null;
            }

            return await _cartRepository.GetCartByUserAsync(user.Id, cancellation);
        }

        private async Task<CartDto> GetCartItemsAsync(Cart cart, CancellationToken cancellation)
        {
            var products = await _productService.GetProductsAsync(new Contracts.Common.PagedRequest(), cancellation);

            var cartItems = new List<CartItemDto>(products.Result.Count);
            var totalAmount = 0m;
            foreach (var productInCart in cart.Products)
            {
                var product = products.Result.FirstOrDefault(x => x.Id == productInCart.ProductId);
                cartItems.Add(new CartItemDto
                {
                    Price = product.Price,
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Quantity = productInCart.Quantity
                });
                totalAmount += product.Price * productInCart.Quantity;
            }

            return new CartDto
            {
                Items = cartItems,
                TotalAmount = Math.Round(totalAmount, 2)
            };
        }        
    }
}
