using Microsoft.Extensions.Configuration;
using OnlineStore.AppServices.Orders.Repositories;
using OnlineStore.DataAccess.Common;
using OnlineStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.DataAccess.Orders.Repositories
{
    public sealed class OrdersRepository : DapperRepositoryBase<Order>, IOrderRepository
    {
        public OrdersRepository(DbContext context) : base(context)
        {
        }
    }
}
