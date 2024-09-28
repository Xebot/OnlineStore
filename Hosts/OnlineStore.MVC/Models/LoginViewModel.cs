using System.ComponentModel.DataAnnotations;

namespace OnlineStore.MVC.Models
{
    /// <summary>
    /// Модель для входа пользователя.
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Е-mail.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; } = default!;

        /// <summary>
        /// Пароль.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = default!;

        /// <summary>
        /// Запомнить меня.
        /// </summary>
        public bool RememberMe { get; set; }
    }
}
