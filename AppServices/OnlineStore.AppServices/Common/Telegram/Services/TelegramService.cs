using Microsoft.Extensions.Options;
using OnlineStore.AppServices.Common.Telegram.Options;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace OnlineStore.AppServices.Common.Telegram.Services
{
    public sealed class TelegramService : ITelegramService
    {
        private readonly ITelegramBotClient _botClient;
        private readonly TelegramBotOptions _options;

        public TelegramService(
            ITelegramBotClient botClient,
            IOptions<TelegramBotOptions> options)
        {
            _botClient = botClient; 
            _options = options.Value;
        }

        public async Task SetWebhookAsync()
        {
            var url = _options.BotWebhookUrl;            
            await _botClient.SetWebhookAsync(
                url: url, 
                certificate: null,
                secretToken: _options.BotToken,
                allowedUpdates: []);
        }

        public async Task DeleteWebhookAsync()
        {
            await _botClient.DeleteWebhookAsync();
        }

        public async Task HandleUpdateAsync(Update update, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                await (update switch
                {
                    { Message: { } message } => OnMessage(message),
                    { EditedMessage: { } message } => OnMessage(message),
                    { CallbackQuery: { } callbackQuery } => ProcessCallbackAsync(update),
                    //{ InlineQuery: { } inlineQuery } => OnInlineQuery(inlineQuery),
                    //{ ChosenInlineResult: { } chosenInlineResult } => OnChosenInlineResult(chosenInlineResult),
                    //{ Poll: { } poll } => OnPoll(poll),
                    //{ PollAnswer: { } pollAnswer } => OnPollAnswer(pollAnswer),
                    // ChannelPost:
                    // EditedChannelPost:
                    // ShippingQuery:
                    // PreCheckoutQuery:
                    _ => UnknownUpdateHandlerAsync(update)
                });
            }
            catch(Exception ex)
            {
                await ErrorHandler(ex, cancellationToken);
            }            
        }

        public async Task<Message> ShowMainMenuAsync(long chatId)
        {
            var inlineKeyboard = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Мой статус заказа", "order_status"),
                    InlineKeyboardButton.WithCallbackData("Техподдержка", "support")
                }
            });

            return await _botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Выберите опцию:",
                replyMarkup: inlineKeyboard);
        }

        public async Task ProcessCallbackAsync(Update update)
        {
            if (update.CallbackQuery != null)
            {
                var callbackData = update.CallbackQuery.Data; // Получаем данные из Callback
                var chatId = update.CallbackQuery.Message.Chat.Id;

                if (callbackData == "order_status")
                {
                    // Обрабатываем получение статуса заказа
                    await _botClient.SendTextMessageAsync(chatId, "Ваш заказ в обработке.");
                }
                else if (callbackData == "support")
                {
                    // Обрабатываем сообщение для техподдержки
                    await _botClient.SendTextMessageAsync(chatId, "Напишите ваше сообщение для техподдержки.");
                }
            }
        }

        private async Task OnMessage(Message msg)
        {
            if (msg.Text is not { } messageText)
                return;

            Message sentMessage = await (messageText.Split(' ')[0] switch
            {
                //"/photo" => SendPhoto(msg),
                //"/inline_buttons" => SendInlineKeyboard(msg),
                //"/keyboard" => SendReplyKeyboard(msg),
                //"/remove" => RemoveKeyboard(msg),
                //"/request" => RequestContactAndLocation(msg),
                //"/inline_mode" => StartInlineQuery(msg),
                //"/poll" => SendPoll(msg),
                //"/poll_anonymous" => SendAnonymousPoll(msg),
                //"/throw" => FailingHandler(msg),
                "/start" => ShowMainMenuAsync(msg.Chat.Id),
                "/code" => ProcessUserCodeAsync(msg),
                _ => Usage(msg)
            });
        }

        private async Task<Message> ProcessUserCodeAsync(Message msg)
        {
            var splitedMessage = msg.Text.Split(' ');

            if (splitedMessage.Length < 2)
            {
                return await _botClient.SendTextMessageAsync(
                chatId: msg.Chat.Id,
                text: $"Не удалось распознать код");
            }

            //вызов UserService и связывание ChatId с User.

            var code = splitedMessage[1];

            return await _botClient.SendTextMessageAsync(
                chatId: msg.Chat.Id,
                text: $"Ваш код {code} обработан успешно");
        }

        private Task UnknownUpdateHandlerAsync(Update update)
        {
            return Task.CompletedTask;
        }

        async Task<Message> Usage(Message msg)
        {
            const string usage = """
                <b><u>Bot menu</u></b>:
                /photo          - send a photo
                /inline_buttons - send inline buttons
                /keyboard       - send keyboard buttons
                /remove         - remove keyboard buttons
                /request        - request location or contact
                /inline_mode    - send inline-mode results list
                /poll           - send a poll
                /poll_anonymous - send an anonymous poll
                /throw          - what happens if handler fails
            """;
            return await _botClient.SendTextMessageAsync(msg.Chat, usage, parseMode: ParseMode.Html, replyMarkup: new ReplyKeyboardRemove());
        }

        private static Task ErrorHandler(Exception error, CancellationToken cancellationToken)
        {
            // Тут создадим переменную, в которую поместим код ошибки и её сообщение 
            var ErrorMessage = error switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => error.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
    }
}
