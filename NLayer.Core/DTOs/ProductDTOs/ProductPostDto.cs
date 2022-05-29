using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.ProductDTOs
{
    public class ProductPostDto
    {
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Explain { get; set; }
        public List<IFormFile> Pictures { get; set; }
        public List<ProductFeatureDto> ProductFeatures { get; set; }
        public List<ProductFeatureDetailDto> CategoryFeatures { get; set; }
    }

}
