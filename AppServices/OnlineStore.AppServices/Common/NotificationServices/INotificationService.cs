using OnlineStore.Contracts.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.AppServices.Common.NotificationServices
{
    public interface INotificationService
    {
        /// <summary>
        /// Отправляет уведомление.
        /// </summary>
        /// <param name="notification">Уведомление.</param>
        /// <param name="cancellation">Токен отмены операции.</param>
        Task SendNotificationAsync(NotificationDto notification, CancellationToken cancellation);
    }
}
