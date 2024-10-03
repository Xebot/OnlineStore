using OnlineStore.Domain.Events;

namespace OnlineStore.AppServices.Common.Events.Common
{
    /// <summary>
    /// Интерфейс накопителя доменных событий.
    /// </summary>
    public interface IEventAccumulator
    {
        /// <summary>
        /// Добавляет событие в накопитель.
        /// </summary>
        /// <param name="domainEvent">Доменное событие.</param>
        void AddEvent(IDomainEvent domainEvent);

        /// <summary>
        /// Возвращает события из накопителя.
        /// </summary>
        IReadOnlyCollection<IDomainEvent> GetAllEvents();

        /// <summary>
        /// Очищает накопитель.
        /// </summary>
        void ClearEvents();
    }
}
