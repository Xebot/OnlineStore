using Microsoft.AspNetCore.Identity;

namespace OnlineStore.Domain.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        /// <summary>
        /// Идентификатор чата с пользователем в Telegram.
        /// </summary>
        public long? TelegramChatId { get; set; }

        /// <summary>
        /// Каналы уведомления пользователя.
        /// </summary>
        public ICollection<NotificationChannel> NotificationChannels { get; set; } = [];
    }
}
