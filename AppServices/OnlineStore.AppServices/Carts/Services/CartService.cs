using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using OnlineStore.AppServices.Carts.Repositories;
using OnlineStore.AppServices.Common.DateTimeProviders;
using OnlineStore.Contracts.Enums;
using OnlineStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public CartService(ICartRepository cartRepository,
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor,
            IDateTimeProvider dateTimeProvider)
        {
            _cartRepository = cartRepository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _dateTimeProvider = dateTimeProvider;
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

                AddPoductToCart(existingCart, productId);

                await _cartRepository.AddAsync(existingCart, cancellation);
            }
            else
            {
                existingCart.Updated = _dateTimeProvider.UtcNow;
                AddPoductToCart(existingCart, productId);
                await _cartRepository.UpdateAsync(existingCart, cancellation);
            }
        }        

        public async Task<int?> GetCartItemCountAsync(CancellationToken cancellation)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            if (user == null)
            {
                return null;
            }

            var existingCart = await _cartRepository.GetCartByUserAsync(user.Id, cancellation);

            return existingCart?.Products?.Sum(x => x.Quantity) ?? 0;
        }

        private static void AddPoductToCart(Cart cart, int productId)
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
    }
}
