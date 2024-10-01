using OnlineStore.Contracts.Products;

namespace OnlineStore.ApiClient
{
    /// <summary>
    /// Интерфейс Апи-клиента.
    /// </summary>
    public interface IOnlineStoreApiClient
    {
        /// <summary>
        /// Добавляет новый товар.
        /// </summary>
        /// <param name="productDto">Транспортная модель товара.</param>
        /// <param name="cancellation">Токен отмены операции.</param>
        Task AddProductAsync(ShortProductDto productDto, CancellationToken cancellation);
    }
}
