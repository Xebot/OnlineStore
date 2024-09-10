using Microsoft.EntityFrameworkCore;
using OnlineStore.DataAccess.Configurations;
using OnlineStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.DataAccess.Common
{
    public class OnlineStoreDbContext : DbContext
    {
        public OnlineStoreDbContext() : base()
        {
            
        }

        public OnlineStoreDbContext(DbContextOptions<OnlineStoreDbContext> options) 
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OnlineStoreDbContext).Assembly);
        }

        DbSet<ProductAttribute> Attributes { get; set; }
    }
}
