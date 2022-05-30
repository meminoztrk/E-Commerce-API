using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public int? BrandId { get; set; }
        public Brand Brand { get; set; }
        public ICollection<ProductFeature> ProductFeatures { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
        public ICollection<FeatureDetail> FeatureDetails { get; set; }
    }
}
