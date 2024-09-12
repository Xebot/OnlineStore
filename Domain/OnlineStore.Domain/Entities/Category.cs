namespace OnlineStore.Domain.Entities
{
    /// <summary>
    /// Категория.
    /// </summary>
    public sealed class Category
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
        /// Идентификатор родительской категории.
        /// </summary>
        public int? ParentCategoryId { get; set; }

        /// <summary>
        /// Родительская категория.
        /// </summary>
        public Category? ParentCategory { get; set; }
    }
}
