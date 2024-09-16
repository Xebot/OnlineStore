namespace OnlineStore.Domain.Entities
{
    /// <summary>
    /// Товар.
    /// </summary>
    public sealed class Product
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
        /// Описание.
        /// </summary>
        public string Description { get; set; } = default!;

        /// <summary>
        /// Цена.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Идентификатор категории.
        /// </summary>
        public int? CategoryId { get; set; }

        /// <summary>
        /// Ссылка на главное изображение товара.
        /// </summary>
        public string? ImageUrl { get; set; }

        /// <summary>
        /// Количество товара.
        /// </summary>
        public int StockQuantity { get; set; }

        /// <summary>
        /// Дата создания.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Дата модификации.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Категория.
        /// </summary>
        public Category? Category { get; set; }

        /// <summary>
        /// Изображения товара.
        /// </summary>
        public ICollection<ProductImage> Images { get; set; } = [];
    }
}
