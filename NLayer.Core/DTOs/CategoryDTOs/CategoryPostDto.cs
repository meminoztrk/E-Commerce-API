using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.CategoryDTOs
{
    public class CategoryPostDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SubId { get; set; }
        public bool IsActive { get; set; }
    }
}
