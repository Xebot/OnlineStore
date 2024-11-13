namespace OnlineStore.Contracts.Common
{
    /// <summary>
    /// Пагинированный ответ.
    /// </summary>
    public class PagedResponse<T>
    {
        /// <summary>
        /// Общее количество.
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Номер текущей страницы.
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Размер страницы.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Сущности.
        /// </summary>
        public List<T> Result { get; set; } = [];
    }
}
