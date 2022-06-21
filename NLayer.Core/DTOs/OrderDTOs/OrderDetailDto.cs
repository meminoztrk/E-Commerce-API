using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.OrderDTOs
{
    public class OrderDetailDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Color { get; set; }
        public int ProductId { get; set; }
        public string Path { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public decimal Total { get; set; }
    }
}
