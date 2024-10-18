using MassTransit;
using OnlineStore.ApiClient;
using OnlineStore.Contracts.BusMessages;

namespace OnlineStore.MessageQueue.Daemon.Consumers
{
    public sealed class TestBusMessageConsumer : IConsumer<TestBusMessage>
    {
        private readonly IOnlineStoreApiClient _onlineStoreApiClient;

        public TestBusMessageConsumer(IOnlineStoreApiClient onlineStoreApiClient)
        {
            _onlineStoreApiClient = onlineStoreApiClient;
        }

        public async Task Consume(ConsumeContext<TestBusMessage> context)
        {
            await _onlineStoreApiClient.AddProductAsync(new Contracts.Products.ShortProductDto
            {
                Name = "Test",
                Description = "Test",
                Price = 1,
                StockQuantity = 1,
                ImageUrl = "Test",
            }, context.CancellationToken);
        }
    }
}
