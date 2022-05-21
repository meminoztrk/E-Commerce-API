using NLayer.Core.DTOs.CategoryDTOs;
using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Repositories
{
    public interface IFeatureRepository:IGenericRepository<CategoryFeature>
    {
        Task<List<CategoryFeature>> GetUndeletedCategoryFeatureAsync();
        Task<List<CategoryWithName>> GetLastCategoriesAsync();
    }
}
