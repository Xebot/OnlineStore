using OnlineStore.Contracts.Enums;

namespace OnlineStore.AppServices.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class NotificationChannelAttribute : Attribute
    {
        public NotificationChannelEnum Channel { get; }

        public NotificationChannelAttribute(NotificationChannelEnum channel)
        {
            Channel = channel;
        }
    }
}
