﻿using AutoMapper;
using NLayer.Core;
using NLayer.Core.DTOs;
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
            CreateMap<ProductFeature,ProductFeature>().ReverseMap();
            CreateMap<Category,CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryWithProductsDto>();

        }
    }
}