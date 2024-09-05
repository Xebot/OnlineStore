namespace OnlineStore.Domain.Entities
{
    /// <summary>
    /// Аттрибут товара.
    /// </summary>
    [Dapper.Contrib.Extensions.Table("Attributes")]
    public class ProductAttribute
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование.
        /// </summary>
        public string name { get; set; }
    }
}
