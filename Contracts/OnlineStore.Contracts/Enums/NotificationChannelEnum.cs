using System.ComponentModel;

namespace OnlineStore.Contracts.Enums
{
    /// <summary>
    /// Способы уведомления пользователя.
    /// </summary>
    public enum NotificationChannelEnum
    {
        [Description("Email")]
        Email = 1,

        [Description("Telegram")]
        Telegram = 2,

        [Description("Личный кабинет")]
        PrivateCabinet = 3
    }
}
