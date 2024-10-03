using OnlineStore.AppServices.Common.Events.Common;
using OnlineStore.Domain.Events;

namespace OnlineStore.DataAccess.Events
{
    /// <summary>
    /// Накопитель доменных событий.
    /// </summary>
    public sealed class EventAccumulator : IEventAccumulator
    {
        private readonly List<IDomainEvent> _events = [];

        /// <inheritdoc/>
        public void AddEvent(IDomainEvent domainEvent) 
            => _events.Add(domainEvent);

        /// <inheritdoc/>
        public void ClearEvents() 
            => _events.Clear();

        /// <inheritdoc/>
        public IReadOnlyCollection<IDomainEvent> GetAllEvents() 
            => _events.AsReadOnly();
    }
}
