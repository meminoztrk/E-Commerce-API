using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.CartDTOs
{
    public class CartAddDto
    {
        public int UserId { get; set; }
        public CartDto Cart { get; set; }
    }
}
