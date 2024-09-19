using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Infrastructure.Extensions
{
    public static class EnumExtensions
    {
        public static string GetEnumDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = field.GetCustomAttribute<DescriptionAttribute>();
            return attribute == null ? value.ToString() : attribute.Description;
        }

        //public static void HasDataFromEnum<TEntity, TEnum>(this EntityTypeBuilder<TEntity> builder)
        //where TEntity : class, new()
        //where TEnum : Enum
        //{
        //    // Проверка, что TEntity имеет свойства Id и Name
        //    if (typeof(TEntity).GetProperty("Id") == null || typeof(TEntity).GetProperty("Name") == null)
        //    {
        //        throw new InvalidOperationException("Entity type must have 'Id' and 'Name' properties.");
        //    }

        //    builder.HasData(
        //        Enum.GetValues(typeof(TEnum))
        //            .Cast<TEnum>()
        //            .Select(e => new TEntity
        //            {
        //                Id = (int)(object)e,
        //                Name = e.GetEnumDescription() // Используем метод для получения Description
        //            })
        //    );
        //}
    }
}
