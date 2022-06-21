using NLayer.Core.DTOs;
using NLayer.Core.DTOs.OrderDTOs;
using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Services
{
    public interface IOrderService:IService<Order>
    {
        Task<CustomResponseDto<List<OrderDto>>> GetOrderWithDetailByUserId(int id);
        Task<CustomResponseDto<List<OrderDto>>> GetUndeletedOrders();
        Task<CustomResponseDto<NoContentDto>> SaveOrder(int userid);
    }
}
