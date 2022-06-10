using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.ProductDTOs
{
    public class ProductFeatureWithIdDto
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public string Color { get; set; }
        public int Stock { get; set; }
        public decimal FePrice { get; set; }
    }
}
