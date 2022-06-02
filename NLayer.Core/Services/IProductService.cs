using NLayer.Core.DTOs;
using NLayer.Core.DTOs.FeatureDTOs;
using NLayer.Core.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Services
{
    public interface IProductService : IService<Product>
    {
        Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductWithCategory();
        Task<CustomResponseDto<List<ProductCatChildDto>>> GetCategoryWithChild();
        Task<CustomResponseDto<List<CategoryFeatureWithNameDto>>> GetCategoryFeaturesByCategoryId(int id);
        Task<CustomResponseDto<NoContentDto>> SaveProduct(ProductPostDto product);
        Task<CustomResponseDto<List<ProductListDto>>> GetUndeletedProductAsync();
        Task<CustomResponseDto<ProductForEditDto>> GetProduct(int id);
    }
}
