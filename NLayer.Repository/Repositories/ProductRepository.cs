using Microsoft.EntityFrameworkCore;
using NLayer.Core;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<CategoryFeature>> GetCategoryFeaturesByCategoryId(int id)
        {
            return await _context.CategoryFeatures.Where(x=>x.CategoryId == id && x.IsActive == true && x.IsDeleted == false).ToListAsync();
        }

        public async Task<List<Product>> GetProductWithCategory()
        {
            return await _context.Products.Include(x=> x.Category).ToListAsync();
        }
        public async Task<List<Product>> GetUndeletedProductAsync()
        {
            return await _context.Products.Include(x => x.Category).Include(x => x.Brand).Where(x => x.IsDeleted == false).ToListAsync();
        }
    }
}
