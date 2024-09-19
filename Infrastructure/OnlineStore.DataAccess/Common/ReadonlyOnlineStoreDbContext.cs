using Microsoft.EntityFrameworkCore;

namespace OnlineStore.DataAccess.Common
{
    public class ReadonlyOnlineStoreDbContext : DbContext
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
