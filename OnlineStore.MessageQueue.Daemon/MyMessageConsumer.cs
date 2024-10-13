using MassTransit;
using OnlineStore.ApiClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.MessageQueue.Daemon
{
    public class MyMessageConsumer : IConsumer<MyMessage>
    {
        private readonly IOnlineStoreApiClient _onlineStoreApiClient;

        public MyMessageConsumer(IOnlineStoreApiClient onlineStoreApiClient)
        {
            _onlineStoreApiClient = onlineStoreApiClient;
        }

        public async Task Consume(ConsumeContext<MyMessage> context)
        {
            // Здесь вызывается другой API
            var message = context.Message;
            Console.WriteLine($"Received message: {message.Text}");

            //await _onlineStoreApiClient.AddProductAsync(new Contracts.Products.ShortProductDto
            //{
            //    Id = 1,
            //    Name = message.Text,
            //    Description = message.Text,
            //    Price = 1,
            //    StockQuantity = 1,
            //    ImageUrl = ""
            //}, context.CancellationToken);

            // Пример вызова другого API
            // await CallOtherApi(message.Text);
        }

        private Task CallOtherApi(string text)
        {
            // Реализация вызова другого API
            return Task.CompletedTask;
        }
    }

    public class MyMessage
    {
        public string Text { get; set; }
    }
}
