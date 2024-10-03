namespace OnlineStore.Domain.Events
{
    /// <summary>
    /// Событие добавления нового товара.
    /// </summary>
    public sealed class AddProductEvent : IDomainEvent
    {
        public DateTime EventDate { get ; set ; }

        /// <summary>
        /// Наименование товара.
        /// </summary>
        public string ProductName { get; set; } = default!;
    }
}
