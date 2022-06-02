using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.ProductDTOs
{
    public class ProductForEditDto
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public List<int> CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public List<ProductFeatureDto> ProductFeatures { get; set; }
        public List<ProductFeatureDetailWithNameDto> CategoryFeaturesDetails { get; set; }
        public List<ProductImageDto> Images { get; set; }

    }
}
