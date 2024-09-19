namespace OnlineStore.Domain.Entities
{
    /// <summary>
    /// Изображение товара.
    /// </summary>
    public sealed class ProductImage
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование.
        /// </summary>
        public string Name { get; set; } = default!;

        /// <summary>
        /// Ссылка.
        /// </summary>
        public string Url { get; set; } = default!;

        /// <summary>
        /// Идентификатор товара.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Товар.
        /// </summary>
        public Product Product { get; set; } = default!;

        public int Idd { get; set; }
    }
}
