using NLayer.Core.DTOs.BrandDTOs;
using NLayer.Core.DTOs.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.ProductDTOs
{
    public class ProductListDto
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public int Stock { get; set; }
        public BrandNameDto Brand { get; set; }
        public CategoryWithName Category { get; set; }
        public bool IsActive { get; set; }
    }
}
