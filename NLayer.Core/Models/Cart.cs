using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Models
{
    public class Cart:BaseEntity
    {
        public int Quantity { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int ProductFeatureId { get; set; }
        public ProductFeature ProductFeature { get; set; }
    }
}
