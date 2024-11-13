using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.AppServices.MessageQueue.Services
{
    public interface IMessageQueueService
    {
        Task SendMessageAsync(object message, CancellationToken cancellation);
    }
}
