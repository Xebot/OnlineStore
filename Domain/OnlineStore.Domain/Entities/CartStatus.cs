namespace OnlineStore.Domain.Entities
{
    /// <summary>
    /// Статус корзины.
    /// </summary>
    public sealed class CartStatus
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
