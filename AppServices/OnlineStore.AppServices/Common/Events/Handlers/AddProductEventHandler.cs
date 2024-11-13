using OnlineStore.AppServices.Common.Events.Common;
using OnlineStore.AppServices.Common.NotificationServices;
using OnlineStore.AppServices.MessageQueue.Services;
using OnlineStore.Contracts.Enums;
using OnlineStore.Domain.Events;

namespace OnlineStore.AppServices.Common.Events.Handlers
{
    /// <summary>
    /// Обработчик события создания товара.
    /// </summary>
    public sealed class AddProductEventHandler : IDomainEventHandler<AddProductEvent>
    {
        private readonly INotificationService _notificationService;
        //private readonly IMessageQueueService _messageQueueService;

        public AddProductEventHandler(
            INotificationService notificationService)
            //IMessageQueueService messageQueueService)
        {
            _notificationService = notificationService;
            //_messageQueueService = messageQueueService;
        }

        /// <inheritdoc/>
        public Task HandleAsync(AddProductEvent @event)
        {
            //return _messageQueueService.SendMessageAsync(new object(), CancellationToken.None);
            return _notificationService.SendNotificationAsync(new Contracts.Notifications.NotificationDto
            {
                Theme = $"Добавлен новый товар - {@event.ProductName}",
                Email = "email@email.com",
                Text = $"Добавлен новый товар - {@event.ProductName}",
                NotificationChannels = [NotificationChannelEnum.Email, NotificationChannelEnum.Telegram]
            }, CancellationToken.None);
        }

        /// <inheritdoc/>
        public Task HandleAsync(IDomainEvent @event)
        {
            return HandleAsync((AddProductEvent)@event);
        }
    }
}
