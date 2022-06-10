using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.ProductDTOs
{
    public class ProductISingleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public List<string> Pictures { get; set; }
        public List<ProductFeatureWithIdDto> Features { get; set; }
        public List<ProductINavigationDto> Navigation { get; set; }
    }
}
