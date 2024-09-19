using OnlineStore.AppServices.Common;
using OnlineStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.AppServices.Asparagus
{
    public interface IAsparagusLoverRepository : IRepository<AsparagusLover>
    {
        Task AddOrUpdateAsync(AsparagusLover entity);

        Task<AsparagusLover> GetByEmailAsync(string email);
    }
}
