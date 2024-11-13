using MimeKit;
using OnlineStore.Contracts.Notifications;
using MailKit.Net.Smtp;
using OnlineStore.AppServices.Common.Attributes;
using OnlineStore.Contracts.Enums;

namespace OnlineStore.AppServices.Common.NotificationServices
{
    /// <summary>
    /// Сервис отправки уведомлений на Email.
    /// </summary>
    [NotificationChannel(NotificationChannelEnum.Email)]
    public sealed class EmailNotificationStrategy : INotificationStrategy
    {
        /// <inheritdoc/>
        public async Task SendNotificationAsync(NotificationDto notification, CancellationToken cancellation)
        {
            using var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта", "onlinestoreapp@outlook.com"));
            emailMessage.To.Add(new MailboxAddress("", notification.Email));
            emailMessage.Subject = notification.Theme;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = notification.Text
            };

            using var client = new SmtpClient();
            await client.ConnectAsync("smtp-mail.outlook.com", 587, MailKit.Security.SecureSocketOptions.StartTls, cancellation);
            await client.AuthenticateAsync("onlinestoreapp@outlook.com", "Test@12345!", cancellation);
            await client.SendAsync(emailMessage, cancellation);

            await client.DisconnectAsync(true, cancellation);
        }
    }
}
