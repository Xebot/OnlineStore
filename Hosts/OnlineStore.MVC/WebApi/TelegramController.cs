using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using OnlineStore.AppServices.Common.Telegram.Services;
using OnlineStore.AppServices.Common.Telegram.Options;
using Microsoft.Extensions.Options;

namespace OnlineStore.MVC.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelegramController : ControllerBase
    {
        private readonly ITelegramService _telegramService;
        private readonly TelegramBotOptions _options;

        public TelegramController(
            ITelegramService telegramService,
            IOptions<TelegramBotOptions> options)
        {
            _telegramService = telegramService;
            _options = options.Value;
        }

        [HttpGet("set")]
        public async Task<IActionResult> Set()
        {
            await _telegramService.SetWebhookAsync();
            return Ok();
        }
       
        [HttpPost("update")]
        public async Task<IActionResult> Update(Update update, CancellationToken ct)
        {
            if (Request.Headers["X-Telegram-Bot-Api-Secret-Token"] != _options.BotToken)
                return Forbid();

            await _telegramService.HandleUpdateAsync(update, CancellationToken.None);

            return Ok();
        }
    }
}
