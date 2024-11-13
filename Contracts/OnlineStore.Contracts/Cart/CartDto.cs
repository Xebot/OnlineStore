using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Contracts.Cart
{
    public sealed class CartDto
    {
        public List<CartItemDto> Items { get; set; } = [];

        public decimal TotalAmount { get; set; }
    }
}
