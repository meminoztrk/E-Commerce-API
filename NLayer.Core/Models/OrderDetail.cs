using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Models
{
    public class OrderDetail : BaseEntity
    {
        public int? OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductFeatureId { get; set; }
        public ProductFeature ProductFeature { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
    }
}
