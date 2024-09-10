using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Contracts.Enums
{
    public enum ProductAttributeTypeEnum
    {
        [Description("Используется для поиска")]
        ForSearch = 1,

        [Description("Не используется для поиска")]
        NotForSearch = 2
    }
}
