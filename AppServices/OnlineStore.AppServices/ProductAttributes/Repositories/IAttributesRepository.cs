using OnlineStore.AppServices.Common;
using OnlineStore.Domain.Entities;

namespace OnlineStore.AppServices.Attributes.Repositories
{
    /// <summary>
    /// Интерфейс репозитория по работе с атрибутами.
    /// </summary>
    public interface IAttributesRepository : IRepository<ProductAttribute>
    { }
}
