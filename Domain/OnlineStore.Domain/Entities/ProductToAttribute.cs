namespace OnlineStore.Domain.Entities
{
    /// <summary>
    /// Связь товара с атрибутами.
    /// </summary>
    public sealed class ProductToAttribute
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор продукта.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Идентификатор атрибута.
        /// </summary>
        public int AttributeId { get; set; }

        /// <summary>
        /// Значение.
        /// </summary>
        public string Value { get; set; } = default!;

        /// <summary>
        /// Товар.
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// Атрибут.
        /// </summary>
        public ProductAttribute Attribute { get; set; }
    }
}
