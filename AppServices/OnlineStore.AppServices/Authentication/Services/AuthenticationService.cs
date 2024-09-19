using Microsoft.AspNetCore.Identity;
using OnlineStore.Domain.Entities;

namespace OnlineStore.AppServices.Authentication.Services
{
    /// <summary>
    /// Сервис аутентификации.
    /// </summary>
    public sealed class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        /// <inheritdoc/>
        public AuthenticationService(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        /// <inheritdoc/>
        public async Task<bool> RegisterAsync(string email, string password, CancellationToken cancellation)
        {
            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
            };

            var registeredUser = await _userManager.CreateAsync(user, password);

            return registeredUser != null;
        }

        /// <inheritdoc/>
        public async Task<bool> SignInAsync(string email, string password, CancellationToken cancellation)
        {
            var user = await _userManager.FindByEmailAsync(email)
                ?? throw new UnauthorizedAccessException("Неправильный email или пароль.");

            var isPasswordMatched = await _userManager.CheckPasswordAsync(user, password);
            if (isPasswordMatched)
            {
                await _signInManager.SignInAsync(user, isPersistent: true);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <inheritdoc/>
        public Task SignOutAsync(CancellationToken cancellation)
        {
            return _signInManager.SignOutAsync();
        }
    }
}
