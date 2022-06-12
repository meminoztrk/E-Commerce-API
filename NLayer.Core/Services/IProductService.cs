using NLayer.Core.DTOs;
using NLayer.Core.DTOs.CartDTOs;
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
        Task<CustomResponseDto<NoContentDto>> EditProduct(int id, ProductPostDto product);
        Task<CustomResponseDto<List<ProductListDto>>> GetUndeletedProductAsync();
        Task<CustomResponseDto<ProductForEditDto>> GetProduct(int id);
        Task<CustomResponseDto<ProductIDataDto>> GetProductsByCategoryName(List<string> categories);
        Task<CustomResponseDto<ProductISingleDto>> GetSingleProduct(int id);
        Task<CustomResponseDto<NoContentDto>> AddManyCart(CartAddManyDto manyCart);
        Task<CustomResponseDto<NoContentDto>> AddCart(CartAddDto cart);
        Task<CustomResponseDto<NoContentDto>> DeleteCart(int userId, int productFeatureId);
        Task<CustomResponseDto<List<CartWithImageDto>>> GetCart(int id);
        

    }
}
