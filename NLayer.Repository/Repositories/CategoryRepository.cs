using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using NLayer.Core;
using NLayer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<List<Category>> GetSubCategoriesWithIdAsync(int id)
        {
            return await _context.Categories.Where(x => x.SubId == id && x.Id != x.SubId && x.IsDeleted == false).ToListAsync();
        }

        public async Task<List<Category>> GetCategoryWithSubAsync()
        {
            return await _context.Categories.Where(x => x.Id != x.SubId && x.IsActive == true && x.IsDeleted == false).ToListAsync();
        }
        public async Task<List<Category>> GetAllMainCategoryAsync()
        {
            return await _context.Categories.Where(x=>x.Id == x.SubId && x.IsDeleted == false).ToListAsync();
        }     

        public async Task<Category> GetSingleCategoryByIdWithProductsAsync(int categoryId)
        {
            return await _context.Categories.Include(x=>x.Products).Where(x=>x.Id == categoryId && x.IsDeleted == false).SingleOrDefaultAsync();
        }
    }
}
