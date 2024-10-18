using MassTransit;

namespace OnlineStore.AppServices.MessageQueue.Services
{
    public sealed class MessageQueueService : IMessageQueueService
    {
        public readonly IPublishEndpoint _publishEndpoint;

        public MessageQueueService(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }
        public Task SendMessageAsync(object message, CancellationToken cancellation)
        {
            return _publishEndpoint.Publish(message, cancellation);
        }
    }
}
