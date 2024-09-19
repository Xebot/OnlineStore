using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Domain.Entities;

namespace OnlineStore.DataAccess.Asparagus.Configurations
{
    public sealed class AsparagusConfiguration : IEntityTypeConfiguration<AsparagusLover>
    {
        public void Configure(EntityTypeBuilder<AsparagusLover> builder)
        {
            builder.ToTable(nameof(AsparagusLover));

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired();
            builder
                .Property(x => x.Email)
                .IsRequired();

            builder
                .HasIndex(x => x.Email)
                .IsUnique();            
        }
    }
}
