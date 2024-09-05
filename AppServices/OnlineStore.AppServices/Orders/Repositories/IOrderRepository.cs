using OnlineStore.AppServices.Common;
using OnlineStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.AppServices.Orders.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
    }
}
