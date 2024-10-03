using OnlineStore.Domain.Events;

namespace OnlineStore.AppServices.Common.Events.Common
{
    /// <summary>
    /// Интерфейс обработчика доменного события.
    /// </summary>
    public interface IDomainEventHandler
    {
        /// <summary>
        /// Обрабатывает доменное событие.
        /// </summary>
        /// <param name="event">Доменное событие.</param>
        Task HandleAsync(IDomainEvent @event);
    }

    /// <summary>
    /// Параметризированный обработчик доменного события.
    /// </summary>
    /// <typeparam name="TEvent">Доменное событие.</typeparam>
    public interface IDomainEventHandler<in TEvent> : IDomainEventHandler where TEvent : IDomainEvent
    {
        Task HandleAsync(TEvent @event);
    }
}
