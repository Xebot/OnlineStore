namespace OnlineStore.Domain.Entities
{
    /// <summary>
    /// Способо оповещения пользователя.
    /// </summary>
    public sealed class NotificationChannel
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование.
        /// </summary>
        public string Name { get; set; }
    }
}
