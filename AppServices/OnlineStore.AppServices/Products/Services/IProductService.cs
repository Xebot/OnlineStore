using OnlineStore.Contracts.Common;
using OnlineStore.Contracts.Products;

namespace OnlineStore.AppServices.Products.Services
{
    /// <summary>
    /// Интерфейс сервиса по работе с товарами.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Возвращает список товаров.
        /// </summary>
        /// <param name="request">Запрос на получение списка товаров.</param>
        /// <param name="cancellation">Токен отмены операции.</param>
        Task<ProductsListDto> GetProductsAsync(PagedRequest request, CancellationToken cancellation);

        /// <summary>
        /// Добавляет товар.
        /// </summary>
        /// <param name="productDto">Транспортная модель товара.</param>
        /// <param name="cancellation">Токен отмены операции.</param>
        Task AddProductAsync(ShortProductDto productDto, CancellationToken cancellation);

        /// <summary>
        /// Возвращает информацию о продукте по его идентификатору.
        /// </summary>
        /// <param name="productId">Идентификатор продукта.</param>
        /// <param name="cancellation">Токен отмены операции.</param>
        Task<ShortProductDto> GetProductByIdAsync(int productId, CancellationToken cancellation);
    }
}
