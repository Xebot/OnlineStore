using OnlineStore.Contracts.ProductAttributes;

namespace OnlineStore.AppServices.Attributes.Services
{
    /// <summary>
    /// Интерфейс сервиса по работе с атрибутами продукта.
    /// </summary>
    public interface IProductAttributeService
    {
        /// <summary>
        /// Возвращает атрибут продукта по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        Task<ProductAttributeDto> GetAsync(int id);
    }
}
