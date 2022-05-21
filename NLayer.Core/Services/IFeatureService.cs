using NLayer.Core.DTOs;
using NLayer.Core.DTOs.CategoryDTOs;
using NLayer.Core.DTOs.FeatureDTOs;
using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Services
{
    public interface IFeatureService:IService<CategoryFeature>
    {
        Task<CustomResponseDto<List<CategoryFeatureDto>>> GetUndeletedCategoryFeatureAsync();
        Task<CustomResponseDto<List<CategoryWithName>>> GetLastCategoriesAsync();
    }
}
