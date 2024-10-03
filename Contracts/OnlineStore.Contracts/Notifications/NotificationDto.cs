using OnlineStore.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Contracts.Notifications
{
    public sealed class NotificationDto
    {
        public string Theme { get; set; }

        public string Text { get; set; }

        public int? ChatId { get; set; }

        public string? Email { get; set; }

        public int? UserId { get; set; }

        public List<NotificationChannelEnum> NotificationChannels { get; set; } = [];
    }
}
