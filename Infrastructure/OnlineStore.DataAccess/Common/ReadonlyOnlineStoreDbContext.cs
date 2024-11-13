using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain.Entities;

namespace OnlineStore.DataAccess.Common
{
    public class ReadonlyOnlineStoreDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        //public ReadonlyOnlineStoreDbContext() : base()
        //{

        //}

        public ReadonlyOnlineStoreDbContext(DbContextOptions<ReadonlyOnlineStoreDbContext> options)
            : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReadonlyOnlineStoreDbContext).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
