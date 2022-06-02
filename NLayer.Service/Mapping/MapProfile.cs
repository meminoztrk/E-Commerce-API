using AutoMapper;
using NLayer.Core;
using NLayer.Core.DTOs;
using NLayer.Core.DTOs.BrandDTOs;
using NLayer.Core.DTOs.CategoryDTOs;
using NLayer.Core.DTOs.FeatureDTOs;
using NLayer.Core.DTOs.ProductDTOs;
using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Mapping
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<Product,ProductDto>().ReverseMap();
            CreateMap<ProductUpdateDto, Product>();
            CreateMap<Product, ProductWithCategoryDto>();
            CreateMap<Product, ProductListDto>();
            CreateMap<Product, ProductListDto>().ReverseMap();
            CreateMap<ProductFeature,ProductFeature>().ReverseMap();

            CreateMap<Category,CategoryDto>().ReverseMap();
            CreateMap<Category,CategoryDto>();
            CreateMap<CategoryUpdateDto,Category>().ReverseMap();
            CreateMap<CategoryPostDto,Category>();
            CreateMap<CategoryPostDto,Category>().ReverseMap();
            CreateMap<Category, CategoryWithProductsDto>();
            CreateMap<Category, CategoryWithSubCount>();
            CreateMap<Category, CategoryWithName>();

            CreateMap<User, UserRegisterDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();

            CreateMap<Brand, BrandDto>();
            CreateMap<Brand, BrandDto>().ReverseMap();
            CreateMap<Brand, BrandUpdateDto>();
            CreateMap<Brand, BrandUpdateDto>().ReverseMap();
            CreateMap<Brand, BrandNameDto>();

            CreateMap<CategoryFeature, CategoryFeatureDto>();
            CreateMap<CategoryFeature, CategoryFeatureDto>().ReverseMap();
            CreateMap<CategoryFeature, CategoryFeaturePostDto>();
            CreateMap<CategoryFeature, CategoryFeaturePostDto>().ReverseMap();
            CreateMap<CategoryFeature, CategoryFeatureWithNameDto>();

        }
    }
}
