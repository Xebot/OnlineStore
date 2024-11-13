using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Contracts.Images
{
    public sealed class ImageDto
    {
        public byte[] Data { get; set; }

        public string ContentType { get; set; }
    }
}
