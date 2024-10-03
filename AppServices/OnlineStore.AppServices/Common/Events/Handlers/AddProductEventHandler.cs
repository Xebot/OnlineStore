using OnlineStore.AppServices.Common.Events.Common;
using OnlineStore.AppServices.Common.NotificationServices;
using OnlineStore.Domain.Events;

namespace OnlineStore.AppServices.Common.Events.Handlers
{
    /// <summary>
    /// Обработчик события создания товара.
    /// </summary>
    public sealed class AddProductEventHandler : IDomainEventHandler<AddProductEvent>
    {
        private readonly INotificationService _notificationService;

        public AddProductEventHandler(
            INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        /// <inheritdoc/>
        public Task HandleAsync(AddProductEvent @event)
        {
            return _notificationService.SendNotificationAsync(new Contracts.Notifications.NotificationDto
            {
                Theme = $"Добавлен новый товар - {@event.ProductName}",
                Email = "email@email.com",
                Text = $"Добавлен новый товар - {@event.ProductName}"
            }, CancellationToken.None);
        }

        /// <inheritdoc/>
        public Task HandleAsync(IDomainEvent @event)
        {
            return HandleAsync((AddProductEvent)@event);
        }
    }
}
