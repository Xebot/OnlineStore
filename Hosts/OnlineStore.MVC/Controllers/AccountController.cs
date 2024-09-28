using Microsoft.AspNetCore.Mvc;
using OnlineStore.AppServices.Authentication.Services;
using OnlineStore.MVC.Models;

namespace OnlineStore.MVC.Controllers
{
    /// <summary>
    /// Контролер аутентификации пользователя.
    /// </summary>
    public class AccountController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        /// <inheritdoc/>
        public AccountController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Показывает окно для входа пользователя.
        /// </summary>
        [HttpGet]
        public IActionResult Login()
        {
            return PartialView("_LoginPartial");
        }

        /// <summary>
        /// Осуществляет авторизацию и аутентификацию пользователя.
        /// </summary>
        /// <param name="model">Данные для входа.</param>
        /// <param name="cancellation">Токен отмены операции.</param>
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, CancellationToken cancellation)
        {
            if (await _authenticationService.SignInAsync(model.Email, model.Password, cancellation))
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View();
        }

        /// <summary>
        /// Разлогинивает пользователя.
        /// </summary>
        /// <param name="cancellation">Токен отмены операции.</param>
        public async Task<IActionResult> Logout(CancellationToken cancellation)
        {
            await _authenticationService.SignOutAsync(cancellation);
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Возвращает форму для регистрации пользователя.
        /// </summary>
        [HttpGet]
        public IActionResult Register()
        {
            return PartialView("_RegisterPartial");
        }

        /// <summary>
        /// Осуществляет регистрацию пользователя.
        /// </summary>
        /// <param name="model">Модель с данными для регистрации.</param>
        /// <param name="cancellation">Токен отменыф операции.</param>
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model, CancellationToken cancellation)
        {
            var result = await _authenticationService.RegisterAsync(model.Email, model.Password, cancellation);
            if (result.Succeeded)
            {
                await _authenticationService.SignInAsync(model.Email, model.Password, cancellation);
                return RedirectToAction("Index", "Home");
            }

            var errors = result.Errors?.Select(x => x.Description).ToList() ?? [];

            return PartialView("_RegisterPartial", new RegisterViewModel
            {
                Errors = errors
            });
        }
    }
}
