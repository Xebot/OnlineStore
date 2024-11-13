namespace OnlineStore.Domain.Entities
{
    /// <summary>
    /// Позиция заказа.
    /// </summary>
    public sealed class OrderItem
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор заказа.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Идентификатор товара.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Количество.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Цена.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Товар.
        /// </summary>
        public Product Product { get; set; } = default!;

        /// <summary>
        /// Заказ.
        /// </summary>
        public Order Order { get; set; } = default!;
    }
}
