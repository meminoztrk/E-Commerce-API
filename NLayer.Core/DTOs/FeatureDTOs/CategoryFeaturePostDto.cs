using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.FeatureDTOs
{
    public class CategoryFeaturePostDto
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public bool IsActive { get; set; }
    }
}
