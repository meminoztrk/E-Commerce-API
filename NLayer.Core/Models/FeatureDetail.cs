using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Models
{
    public class FeatureDetail:BaseEntity
    {
        public string Value { get; set; }
        public int? CategoryFeatureId { get; set; }
        public CategoryFeature CategoryFeature { get; set; }
        public int? ProductId { get; set; }
        public Product Product { get; set; }

    }
}
