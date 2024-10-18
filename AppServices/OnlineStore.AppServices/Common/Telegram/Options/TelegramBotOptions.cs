using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.AppServices.Common.Telegram.Options
{
    public sealed class TelegramBotOptions
    {
        public string BotToken { get; init; } = default!;
        public string BotWebhookUrl { get; init; } = default!;
        public string SecretToken { get; init; } = default!;
    }
}
