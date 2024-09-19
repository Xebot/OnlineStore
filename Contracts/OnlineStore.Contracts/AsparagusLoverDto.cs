using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Contracts
{
    public class AsparagusLoverDto
    {
        public string Email {  get; set; }

        public string Name { get; set; }

        public DateTime DateTime { get; set; }

        public int EatCount { get; set; }
    }
}
