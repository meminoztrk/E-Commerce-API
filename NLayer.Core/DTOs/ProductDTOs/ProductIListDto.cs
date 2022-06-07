using NLayer.Core.DTOs.FeatureDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.ProductDTOs
{
    public class ProductIListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Color { get; set; }
        public bool Status { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public  List<FeatureNameValueDto> Features { get; set; }
    }
}
