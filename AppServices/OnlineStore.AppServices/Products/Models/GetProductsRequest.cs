using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.AppServices.Products.Models
{
    public sealed class GetProductsRequest
    {
        public int Take { get; set; }

        public int Skip { get; set; }

        public bool IncludeCategory { get; set; }

        public bool IncludeImages { get; set; }
    }
}
