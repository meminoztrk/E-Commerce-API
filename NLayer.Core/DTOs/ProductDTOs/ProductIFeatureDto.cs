using NLayer.Core.DTOs.BrandDTOs;
using NLayer.Core.DTOs.FeatureDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.ProductDTOs
{
    public class ProductIFeatureDto
    {
        public List<ProductCatChildWithTitleDto> TreeData { get; set; }
        public List<string> Brands { get; set; }
        public List<string> Colors { get; set; }
        public List<CategoryFeatureWithValuesDto> Values { get; set; }
    }
}
