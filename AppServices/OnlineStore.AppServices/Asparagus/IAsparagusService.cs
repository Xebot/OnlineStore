using OnlineStore.AppServices.Common;
using OnlineStore.Contracts;
using OnlineStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.AppServices.Asparagus
{
    public interface IAsparagusService
    {
        Task ProcessAsparagusLoverAsync(string email, string name);

        Task<IReadOnlyCollection<AsparagusLoverDto>> GetList();
    }
}
