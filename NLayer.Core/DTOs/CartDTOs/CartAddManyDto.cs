using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.CartDTOs
{
    public class CartAddManyDto
    {
        public int UserId { get; set; }
        public List<CartDto> Cart { get; set; }
    }
}
