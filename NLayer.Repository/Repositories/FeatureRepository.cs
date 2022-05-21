using Microsoft.EntityFrameworkCore;
using NLayer.Core;
using NLayer.Core.DTOs.CategoryDTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Repositories
{
    public class FeatureRepository : GenericRepository<CategoryFeature>, IFeatureRepository
    {
        public FeatureRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<CategoryWithName>> GetLastCategoriesAsync()
        {
            var Categories = await _context.Categories.Where(x=>x.IsDeleted == false).ToListAsync();
            List<CategoryWithName> lastCategories = new List<CategoryWithName>();
            foreach (var category in Categories)
            {
                if (!Categories.Any(x=>x.SubId == category.Id))
                {
                    lastCategories.Add(new CategoryWithName { Id = category.Id, Name = category.Name });
                }
            }
            return lastCategories;
        }

        public async Task<List<CategoryFeature>> GetUndeletedCategoryFeatureAsync()
        {
            return await _context.CategoryFeatures.Include(x=>x.Category).Where(x=>x.IsDeleted == false).ToListAsync();
        }
    }
}
