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
        Task<CustomResponseDto<List<OrderDto>>> GetPendingOrderWithDetailByUserId(int id);
        Task<CustomResponseDto<List<OrderDto>>> GetCompletedOrderWithDetailByUserId(int id);
        Task<CustomResponseDto<List<OrderDto>>> GetUndeletedPendingOrders();
        Task<CustomResponseDto<List<OrderDto>>> GetUndeletedCompletedOrders();
        Task<CustomResponseDto<NoContentDto>> SaveOrder(int userid);
    }
}
