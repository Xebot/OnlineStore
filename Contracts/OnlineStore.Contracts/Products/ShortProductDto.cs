using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Contracts.Products
{
    /// <summary>
    /// Краткая информация о товаре.
    /// </summary>
    public sealed class ShortProductDto
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        public string Description { get; set; } = default!;

        /// <summary>
        /// Цена.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Доступное количество.
        /// </summary>
        public int StockQuantity { get; set; }

        /// <summary>
        /// Идентификатор категории.
        /// </summary>
        public int? CategoryId { get; set; }

        /// <summary>
        /// Главное изображение.
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Список изображений.
        /// </summary>
        public string[] ImagesUrls { get; set; }
    }
}
