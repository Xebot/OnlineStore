using MimeKit;
using OnlineStore.Contracts.Notifications;
using MailKit.Net.Smtp;

namespace OnlineStore.AppServices.Common.NotificationServices
{
    /// <summary>
    /// Сервис отправки уведомлений на Email.
    /// </summary>
    public sealed class EmailNotificationService : INotificationService
    {
        /// <inheritdoc/>
        public async Task SendNotificationAsync(NotificationDto notification, CancellationToken cancellation)
        {
            using var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта", "login@yandex.ru"));
            emailMessage.To.Add(new MailboxAddress("", notification.Email));
            emailMessage.Subject = notification.Theme;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = notification.Text
            };

            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.yandex.ru", 25, false, cancellation);
            await client.AuthenticateAsync("login@yandex.ru", "password", cancellation);
            await client.SendAsync(emailMessage, cancellation);

            await client.DisconnectAsync(true, cancellation);
        }
    }
}
