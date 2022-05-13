using NLayer.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Services
{
    public interface ICategoryService:IService<Category>
    {
        Task<CustomResponseDto<List<CategoryWithSubCount>>> GetSubCategoriesWithIdAsync(int id);
        Task<CustomResponseDto<List<CategoryWithSubCount>>> GetUnDeletedCategoriesAsync();
        Task<CustomResponseDto<CategoryWithProductsDto>> GetSingleCategoryByIdWithProductsAsync(int categoryId);
        Task<CustomResponseDto<List<CategoryDto>>> GetAllMainCategoryAsync();
        Task<CustomResponseDto<List<CategoryWithSubsDto>>> GetCategoryWithSubAsync();
       
    }
}
