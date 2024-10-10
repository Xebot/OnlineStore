using OnlineStore.AppServices.Common.Attributes;
using OnlineStore.Contracts.Enums;
using OnlineStore.Contracts.Notifications;
using System.Reflection;

namespace OnlineStore.AppServices.Common.NotificationServices
{
    public sealed class NotificationService : INotificationService
    {
        private readonly Dictionary<NotificationChannelEnum, INotificationStrategy> _notificationStrategies;

        public NotificationService(IEnumerable<INotificationStrategy> notificationStrategies)
        {
            _notificationStrategies = notificationStrategies
                .ToDictionary(strategy => strategy.GetType().GetCustomAttribute<NotificationChannelAttribute>()?.Channel ?? NotificationChannelEnum.Email);
        }

        public async Task SendNotificationAsync(NotificationDto notification, CancellationToken cancellation)
        {
            foreach (var channel in notification.NotificationChannels)
            {
                if (_notificationStrategies.TryGetValue(channel, out var strategy))
                {
                    await strategy.SendNotificationAsync(notification, cancellation);
                }
                else
                {
                    throw new InvalidOperationException("Неподдерживаемый канал уведомления");
                }
            }
        }
    }
}
