using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.ProductDTOs
{
    public class ProductCatChildDto
    {
        public int Value { get; set; }
        public string Label { get; set; }
        public ICollection<ProductCatChildDto> Children { get; set; }

    }

}
