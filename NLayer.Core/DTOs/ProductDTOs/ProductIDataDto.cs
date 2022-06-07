using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.ProductDTOs
{
    public class ProductIDataDto
    {
        public List<ProductINavigationDto> Navigation { get; set; }
        public List<ProductIListDto> Products { get; set; }
        public ProductINavDto ProductNav { get; set; }
        public ProductIFeatureDto ProductFeatures { get; set; }
    }
}
