using Microsoft.EntityFrameworkCore;
using OnlineStore.DataAccess.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbMigrator
{
    public sealed class MigrationDbContext : MutableOnlineStoreDbContext
    {
        public MigrationDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
