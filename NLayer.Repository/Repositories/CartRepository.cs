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
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        public CartRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Cart>> GetCartWithUserId(int id)
        {
            return await _context.Carts.Include(x=>x.ProductFeature).ThenInclude(x=>x.Product).ThenInclude(x=>x.ProductImages).Where(x=>x.UserId == id).ToListAsync();
        }
    }
}
