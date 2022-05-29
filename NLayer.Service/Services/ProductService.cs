using AutoMapper;
using NLayer.Core;
using NLayer.Core.DTOs;
using NLayer.Core.DTOs.FeatureDTOs;
using NLayer.Core.DTOs.ProductDTOs;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Services
{
    public class ProductService : Service<Product>, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public ProductService(IGenericRepository<Product> repository, IUnitOfWork unitOfWork, IProductRepository productRepository, ICategoryRepository categoryRepository, IMapper mapper) : base(repository, unitOfWork)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        protected List<ProductCatChildDto> CategoryLoop(Category main)
        {
            List<ProductCatChildDto> categorySub = new List<ProductCatChildDto>();
            var subs = _categoryRepository.Where(x => x.SubId == main.Id && x.Id != x.SubId && x.IsDeleted == false).ToList();
            foreach (var sub in subs)
            {
                categorySub.Add(new ProductCatChildDto { Value = sub.Id, Label = sub.Name, Children = subs.Count != 0 ? CategoryLoop(sub) : null });
            }
            return categorySub;
        }
        
        public async Task<CustomResponseDto<List<ProductCatChildDto>>> GetCategoryWithChild()
        {
            var mainCategories = await _categoryRepository.GetAllMainCategoryAsync();

            List <ProductCatChildDto> category = new List<ProductCatChildDto>();
            foreach (var main in mainCategories)
            {
                category.Add(new ProductCatChildDto { Value = main.Id, Label = main.Name, Children = CategoryLoop(main) });
            }
            return CustomResponseDto<List<ProductCatChildDto>>.Success(200, category);
        }

        public async Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductWithCategory()
        {
            var product = await _productRepository.GetProductWithCategory();

            var productDto =  _mapper.Map<List<ProductWithCategoryDto>>(product);

            return CustomResponseDto<List<ProductWithCategoryDto>>.Success(200, productDto);
        }

        public async Task<CustomResponseDto<List<CategoryFeatureWithNameDto>>> GetCategoryFeaturesByCategoryId(int id)
        {
            var features = await _productRepository.GetCategoryFeaturesByCategoryId(id);

            var featureDto = _mapper.Map<List<CategoryFeatureWithNameDto>>(features);

            return CustomResponseDto<List<CategoryFeatureWithNameDto>>.Success(200, featureDto);
        }
    }
}
