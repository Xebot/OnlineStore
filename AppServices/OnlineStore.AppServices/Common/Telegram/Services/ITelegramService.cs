using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace OnlineStore.AppServices.Common.Telegram.Services
{
    public interface ITelegramService
    {
        Task HandleUpdateAsync(Update update, CancellationToken cancellationToken);

        Task SetWebhookAsync();
    }
}
