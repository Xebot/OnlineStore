using OnlineStore.AppServices.Common.Attributes;
using OnlineStore.Contracts.Enums;
using OnlineStore.Contracts.Notifications;

namespace OnlineStore.AppServices.Common.NotificationServices
{
    [NotificationChannel(NotificationChannelEnum.Telegram)]
    public sealed class TelegramNotificationStrategy : INotificationStrategy
    {
        public Task SendNotificationAsync(NotificationDto notification, CancellationToken cancellation)
        {
            return Task.CompletedTask;
        }
    }
}
