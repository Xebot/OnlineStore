using OnlineStore.Contracts.Notifications;

namespace OnlineStore.AppServices.Common.NotificationServices
{
    /// <summary>
    /// Интерфейс сервиса уведомлений.
    /// </summary>
    public interface INotificationService
    {
        /// <summary>
        /// Отправляет уведомление.
        /// </summary>
        /// <param name="notification">Уведомление.</param>
        /// <param name="cancellation">Токен отмены операции.</param>
        Task SendNotificationAsync(NotificationDto  notification, CancellationToken cancellation);
    }
}
