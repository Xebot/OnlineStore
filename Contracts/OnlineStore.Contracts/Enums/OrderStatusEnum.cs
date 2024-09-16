using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Contracts.Enums
{
    /// <summary>
    /// Статус заказа.
    /// </summary>
    public enum OrderStatusEnum
    {
        /// <summary>
        /// Новый.
        /// </summary>
        [Description("Новый")]
        New = 1,

        /// <summary>
        /// В обработке.
        /// </summary>
        [Description("В обработке")]
        Processing = 2,

        /// <summary>
        /// Подтвержден.
        /// </summary>
        [Description("Подтвержден")]
        Confirmed = 3,

        /// <summary>
        /// Отправлен.
        /// </summary>
        [Description("Отправлен")]
        Shipped = 4,

        /// <summary>
        /// Доставлен.
        /// </summary>
        [Description("Доставлен")]
        Delivered = 5,

        /// <summary>
        /// Отменен.
        /// </summary>
        [Description("Отменен")]
        Canceled = 6,

        /// <summary>
        /// Возвращен.
        /// </summary>
        [Description("Возвращен")]
        Returned = 7,

        /// <summary>
        /// Средства возвращены.
        /// </summary>
        [Description("Средства возвращены")]
        Refunded = 8,

        /// <summary>
        /// В ожидании.
        /// </summary>
        [Description("В ожидании")]
        OnHold = 9,

        /// <summary>
        /// Ожидает оплаты.
        /// </summary>
        [Description("Ожидает оплаты")]
        AwaitingPayment = 10
    }
}
