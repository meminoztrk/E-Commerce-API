using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.ProductDTOs
{
    public class ProductImageDto
    {
        public int Uid { get; set; }
        public string Name { get; set; }
        public string Status => "done";
        public string Url { get; set; }
    }
}
