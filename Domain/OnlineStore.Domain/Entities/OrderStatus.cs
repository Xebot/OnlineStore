namespace OnlineStore.Domain.Entities
{
    /// <summary>
    /// Статус заказа.
    /// </summary>
    public sealed class OrderStatus
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
