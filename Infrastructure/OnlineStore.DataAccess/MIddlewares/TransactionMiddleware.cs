using Microsoft.AspNetCore.Http;
using OnlineStore.AppServices.Common.Events.Common;
using OnlineStore.DataAccess.Common;

namespace OnlineStore.DataAccess.MIddlewares
{
    /// <summary>
    /// Мидлвэар обработки транзакции и пост-действий.
    /// </summary>
    public sealed class TransactionMiddleware
    {
        private readonly RequestDelegate _next;

        // <inheritdoc/>
        public TransactionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // <inheritdoc/>
        public async Task Invoke(HttpContext context, MutableOnlineStoreDbContext dbContext, 
            IEventAccumulator eventAccumulator, IEventDispatcher eventDispatcher)
        {
            await _next(context);

            var hasChanges = dbContext.HasPendingChanges();

            if (hasChanges)
            {
                using var transaction = await dbContext.Database.BeginTransactionAsync();

                await dbContext.SaveChangesAsync();

                await transaction.CommitAsync();
            }

            var events = eventAccumulator.GetAllEvents();

            foreach (var @event in events)
            {
                await eventDispatcher.DispatchAsync(@event);
            }
        }
    }
}
