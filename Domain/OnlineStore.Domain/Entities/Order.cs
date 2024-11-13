namespace OnlineStore.Domain.Entities
{
    /// <summary>
    /// Заказ.
    /// </summary>
    public sealed class Order
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// Дата заказа.
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Сумма заказа.
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Идентификатор статуса заказа.
        /// </summary>
        public int OrderStatusId { get; set; }

        /// <summary>
        /// Статус заказа.
        /// </summary>
        public OrderStatus OrderStatus { get; set; }

        /// <summary>
        /// Позиции заказа.
        /// </summary>
        public ICollection<OrderItem> Items { get; set; } = [];
    }
}
