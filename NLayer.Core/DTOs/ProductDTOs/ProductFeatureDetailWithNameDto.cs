using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.ProductDTOs
{
    public class ProductFeatureDetailWithNameDto
    {
        public int CategoryFeatureId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
