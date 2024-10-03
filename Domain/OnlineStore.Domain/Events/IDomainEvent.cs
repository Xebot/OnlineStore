namespace OnlineStore.Domain.Events
{
    /// <summary>
    /// Доменное событие.
    /// </summary>
    public interface IDomainEvent
    {
        /// <summary>
        /// Время события.
        /// </summary>
        DateTime EventDate { get; set; }
    }
}
