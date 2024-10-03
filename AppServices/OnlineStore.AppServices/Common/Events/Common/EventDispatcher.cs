using Microsoft.Extensions.DependencyInjection;
using OnlineStore.Domain.Events;

namespace OnlineStore.AppServices.Common.Events.Common
{
    /// <summary>
    /// Диспатчер доменных событий.
    /// </summary>
    public sealed class EventDispatcher : IEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        /// <inheritdoc/>
        public EventDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <inheritdoc/>
        public async Task DispatchAsync(IDomainEvent domainEvent)
        {
            var eventType = domainEvent.GetType();
            var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(eventType);

            var handlers = _serviceProvider.GetServices(handlerType);

            foreach (var handler in handlers)
            {
                if (handler == null)
                {
                    continue;
                }

                await ((IDomainEventHandler)handler).HandleAsync(domainEvent);
            }
        }
    }
}
