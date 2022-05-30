using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using NLayer.Core;
using NLayer.Core.DTOs;
using NLayer.Core.DTOs.FeatureDTOs;
using NLayer.Core.DTOs.ProductDTOs;
using NLayer.Core.Models;
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
        private readonly IProductFeatureService _productFeatureService;
        private readonly IFeatureDetailService _featureDetailService;
        private readonly IProductImageService _productImageService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductService(IGenericRepository<Product> repository, ICategoryRepository categoryRepository, IUnitOfWork unitOfWork,
                                                 IProductRepository productRepository, IProductFeatureService productFeatureService, IFeatureDetailService featureDetailService, IProductImageService productImageService,
                                                 IWebHostEnvironment webHostEnvironment, IMapper mapper) : base(repository, unitOfWork)
        {
            _productRepository = productRepository;
            _productFeatureService = productFeatureService;
            _featureDetailService = featureDetailService;
            _categoryRepository = categoryRepository;
            _productImageService = productImageService;
            _webHostEnvironment = webHostEnvironment;
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

        public async Task<CustomResponseDto<NoContentDto>> SaveProduct(ProductPostDto product)
        {
            Product sproduct = new Product();
            sproduct.CategoryId = product.CategoryId;
            sproduct.BrandId = product.BrandId;
            sproduct.Name = product.Name;
            sproduct.Description = product.Explain;
            sproduct.CreatedDate = DateTime.Now;
            sproduct.IsActive = product.IsActive;
            foreach (var item in product.ProductFeatures)
            {
                sproduct.Stock += item.Stock;
            }

            await AddAsync(sproduct);

            List<ProductFeature> productFeatures = new List<ProductFeature>();
            foreach (var item in product.ProductFeatures)
            {
                productFeatures.Add(new ProductFeature()
                {
                    ProductId = sproduct.Id,
                    Color = item.Color,
                    Stock = item.Stock,
                    FePrice = item.FePrice,
                    Status = item.Status == "0" ? "Sıfır" : "İkinci El",
                });
            }

            
            await _productFeatureService.AddRangeAsync(productFeatures);

            List<FeatureDetail> featureDetails = new List<FeatureDetail>();
            foreach (var item in product.CategoryFeatures)
            {
                featureDetails.Add(new FeatureDetail()
                {
                    CategoryFeatureId = item.CategoryFeatureId,
                    ProductId = sproduct.Id,
                    Value = item.Value,
                    IsActive = true
                });
            }

            await _featureDetailService.AddRangeAsync(featureDetails);

            List<ProductImage> productImages = new List<ProductImage>();
            foreach (var item in product.Pictures)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(item.FileName);
                string extensions = Path.GetExtension(item.FileName);
                string now = DateTime.Now.ToString("yymmssfff");
                string path = Path.Combine(wwwRootPath + "/img/product/", fileName + now + extensions);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await item.CopyToAsync(fileStream);
                }

                productImages.Add(new ProductImage()
                {
                    ProductId = sproduct.Id,
                    Path = fileName + now + extensions,
                    IsActive = true
                });
            }

            await _productImageService.AddRangeAsync(productImages);

            return CustomResponseDto<NoContentDto>.Success(200, "Ürün Eklendi");
        }
    }
}
