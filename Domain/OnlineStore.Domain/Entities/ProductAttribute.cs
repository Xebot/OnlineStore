namespace OnlineStore.Domain.Entities
{
    /// <summary>
    /// Аттрибут товара.
    /// </summary>
    public class ProductAttribute
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование.
        /// </summary>
        public string Name { get; set; } = default!;
    }
}
