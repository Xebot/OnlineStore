using Microsoft.EntityFrameworkCore;
using OnlineStore.AppServices.Carts.Repositories;
using OnlineStore.Contracts.Enums;
using OnlineStore.DataAccess.Common;
using OnlineStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.DataAccess.Carts.Repositories
{
    public sealed class CartRepository : EfRepositoryBase<Cart>, ICartRepository
    {
        public CartRepository(
            MutableOnlineStoreDbContext mutableDbContext, 
            ReadonlyOnlineStoreDbContext readOnlyDbContext) 
            : base(mutableDbContext, readOnlyDbContext)
        {
        }

        public Task<Cart> GetCartByUserAsync(int userId, CancellationToken cancellation)
        {
            return MutableDbContext.Set<Cart>()
                .Where(c => c.UserId == userId)
                .Where(c => c.StatusId == (int)CartStatusEnum.New)
                .Include(c => c.Products)
                .FirstOrDefaultAsync(cancellation);
        }
    }
}
