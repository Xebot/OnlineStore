namespace OnlineStore.Domain.Entities
{
    /// <summary>
    /// Связь атрибутов и групп.
    /// </summary>
    public sealed class ProductAttributeToGroup
    {
        /// <summary>
        /// Идентификатор атрибута.
        /// </summary>
        public int ProductAttributeId { get; set; }

        /// <summary>
        /// Идентификатор группы.
        /// </summary>
        public int ProductAttributeGroupId { get; set; }
    }
}
