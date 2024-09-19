using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.Entities
{
    public sealed class AsparagusLover
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public int AsparagusEatCount { get; set; }

        public DateTime DateTime { get; set; }
    }
}
