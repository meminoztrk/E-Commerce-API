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
    internal class FeatureDetailRepository : GenericRepository<FeatureDetail>, IFeatureDetailRepository
    {
        public FeatureDetailRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<FeatureDetail>> GetDetailWithFeatureNameByProductId(int id)
        {
            return await _context.FeatureDetails.Include(x=>x.CategoryFeature).Where(x=>x.ProductId == id).ToListAsync();
        }
    }
}
