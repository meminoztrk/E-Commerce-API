using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Order>> GetOrderWithDetailByUserId(int id)
        {
            return await _context.Orders.Include(x=>x.User).Include(x=>x.OrderDetails).ThenInclude(x => x.ProductFeature).ThenInclude(x => x.Product).ThenInclude(x=>x.ProductImages).Where(x=>x.UserId == id && x.IsActive == true && x.IsDeleted == false).ToListAsync();
        }

        public async Task<List<Order>> GetUndeletedOrders()
        {
            return await _context.Orders.Include(x => x.User).Include(x => x.OrderDetails).ThenInclude(x=>x.ProductFeature).ThenInclude(x=>x.Product).ThenInclude(x => x.ProductImages).Where(x => x.IsActive == true && x.IsDeleted == false).ToListAsync();
        }
    }
}
