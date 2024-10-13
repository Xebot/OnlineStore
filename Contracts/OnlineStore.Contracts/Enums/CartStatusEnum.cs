using System.ComponentModel;


namespace OnlineStore.Contracts.Enums
{
    /// <summary>
    /// Статус корзины.
    /// </summary>
    public enum CartStatusEnum
    {
        /// <summary>
        /// Новая.
        /// </summary>
        [Description("Новая")]
        New = 1,

        /// <summary>
        /// Оформлена.
        /// </summary>
        [Description("Оформлена")]
        Done = 2,
    }
}
