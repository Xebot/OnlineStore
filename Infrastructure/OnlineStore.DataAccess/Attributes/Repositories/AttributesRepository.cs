using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using OnlineStore.AppServices.Attributes.Repositories;
using OnlineStore.DataAccess.Common;
using System.Data;
using System.Data.Common;

namespace OnlineStore.DataAccess.Attributes.Repositories
{
    /// <summary>
    /// Репозиторий по работе с атрибутами.
    /// </summary>
    public sealed class AttributesRepository : DapperRepositoryBase<OnlineStore.Domain.Entities.ProductAttribute>, IAttributesRepository
    {
        public AttributesRepository(DbContext context) : base(context)
        {
        }
    }
}
