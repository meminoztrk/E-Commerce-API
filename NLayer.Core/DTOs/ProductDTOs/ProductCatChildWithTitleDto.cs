using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.ProductDTOs
{
    public class ProductCatChildWithTitleDto
    {
        public string Key { get; set; }
        public string Title { get; set; }
        public List<ProductCatChildWithTitleDto> Children { get; set; }
    }
}
