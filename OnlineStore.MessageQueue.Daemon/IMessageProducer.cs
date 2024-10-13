using MassTransit;

namespace OnlineStore.MessageQueue.Daemon
{
    public interface IMessageProducer
    {
        Task PublishMessage(object message);
    }

    public class MessageProducer : IMessageProducer
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public MessageProducer(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task PublishMessage(object text)
        {
            var message = new MyMessage { Text = text.ToString() };
            await _publishEndpoint.Publish(message);
            Console.WriteLine($"Отправлено сообщение: {message.Text}");
        }
    }
}
