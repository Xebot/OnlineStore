using OnlineStore.AppServices.Common;
using OnlineStore.Domain.Entities;

namespace OnlineStore.AppServices.Carts.Repositories
{
    public interface ICartRepository : IRepository<Cart>
    {
        Task<Cart> GetCartByUserAsync(int userId, CancellationToken cancellation);
    }
}
