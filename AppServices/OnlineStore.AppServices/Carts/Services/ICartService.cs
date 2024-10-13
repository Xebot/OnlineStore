namespace OnlineStore.AppServices.Carts.Services
{
    /// <summary>
    /// Интерфейс сервиса по работе с корзиной.
    /// </summary>
    public interface ICartService
    {
        /// <summary>
        /// Добавляет товар в корзину текущего пользователя.
        /// </summary>
        /// <param name="productId">Идентификатор товара.</param>
        /// <param name="cancellation">Токен отмены операции.</param>
        /// <returns></returns>
        Task AddProductToCartAsync(int productId, CancellationToken cancellation);

        /// <summary>
        /// Возвращает количество товара у текущего пользователя.
        /// </summary>
        /// <param name="cancellation">Токен отмены операции.</param>
        Task<int?> GetCartItemCountAsync(CancellationToken cancellation);
    }
}
