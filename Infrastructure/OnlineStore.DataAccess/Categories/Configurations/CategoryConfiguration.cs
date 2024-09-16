using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Domain.Entities;

namespace OnlineStore.DataAccess.Categories.Configurations
{
    public sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("category");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name)
                .IsRequired(true);

            builder.HasData(new Category
            {
                Id = 1,
                Name = "Гитары"
            },
            new Category
            {
                Id = 2,
                Name = "Клавишные"
            },
            new Category
            {
                Id = 3,
                Name = "Ударные"
            },
            new Category
            {
                Id = 4,
                Name = "Бас гитары",
                ParentCategoryId = 1
            },
            new Category
            {
                Id = 5,
                Name = "Электрогитары",
                ParentCategoryId = 1
            });
        }
    }
}
