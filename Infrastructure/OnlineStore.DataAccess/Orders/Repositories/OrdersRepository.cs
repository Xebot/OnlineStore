using OnlineStore.AppServices.Orders.Repositories;
using OnlineStore.DataAccess.Common;
using OnlineStore.Domain.Entities;

namespace OnlineStore.DataAccess.Orders.Repositories
{
    /// <summary>
    /// Репозиторий по работе с заказами.
    /// </summary>
    public sealed class OrdersRepository : EfRepositoryBase<Order>, IOrderRepository
    {
        public OrdersRepository(MutableOnlineStoreDbContext mutableDbContext, ReadonlyOnlineStoreDbContext readOnlyDbContext) : base(mutableDbContext, readOnlyDbContext)
        {
        }
    }
}
