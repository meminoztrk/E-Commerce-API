using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Repositories
{
    public interface IOrderRepository:IGenericRepository<Order>
    {
        Task<List<Order>> GetPendingOrderWithDetailByUserId(int id);
        Task<List<Order>> GetCompletedOrderWithDetailByUserId(int id);
        Task<List<Order>> GetUndeletedPendingOrders();
        Task<List<Order>> GetUndeletedCompletedOrders();
    }
}
