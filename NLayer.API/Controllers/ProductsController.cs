using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NLayer.API.Filters;
using NLayer.Core;
using NLayer.Core.DTOs;
using NLayer.Core.DTOs.ProductDTOs;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    public class ProductsController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(IMapper mapper, IProductService productService, IWebHostEnvironment webHostEnvironment)
        {
            _mapper = mapper;
            _productService = productService;
           _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetProductsByCategoryName([FromBody] List<string> categories)
        {
            return CreateActionResult(await _productService.GetProductsByCategoryName(categories));
        }

        [HttpGet("[action]")]
        public IActionResult GetImage(string path)
        {
            string imagePath = _webHostEnvironment.WebRootPath + "/img/product/" + path;
            Byte[] image = System.IO.File.ReadAllBytes(imagePath); 
            return File(image, "image/png");
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetProduct(int id)
        {
            return CreateActionResult(await _productService.GetProduct(id));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetUndeletedProducts()
        {
            return CreateActionResult(await _productService.GetUndeletedProductAsync());
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> EditProduct(int id,[FromForm] ProductPostDto product)
        {
            return CreateActionResult(await _productService.EditProduct(id,product));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SaveProduct([FromForm]ProductPostDto product)
        {
            return CreateActionResult(await _productService.SaveProduct(product));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetCategoryFeaturesByCategoryId(int id)
        {
            return CreateActionResult(await _productService.GetCategoryFeaturesByCategoryId(id));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetCategoryWithChild()
        {
            return CreateActionResult(await _productService.GetCategoryWithChild());
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductWithCategory()
        {
            return CreateActionResult(await _productService.GetProductWithCategory());
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> Any(string product)
        {
            bool products = await _productService.AnyAsync(x=>x.Name == product);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(200,"Böyle bir ürün mevcut"));
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var products = await _productService.GetAllAsync();
            var productsDtos = _mapper.Map<List<ProductDto>>(products.ToList());

            return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));
        }

        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            var productsDto = _mapper.Map<ProductDto>(product);

            return CreateActionResult(CustomResponseDto<ProductDto>.Success(200, productsDto));
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            var product = await _productService.AddAsync(_mapper.Map<Product>(productDto));
            var productsDto = _mapper.Map<ProductDto>(product);

            return CreateActionResult(CustomResponseDto<ProductDto>.Success(201, productsDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto productDto)
        {
            await _productService.UpdateAsync(_mapper.Map<Product>(productDto));

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpPatch("{id}")]

        public async Task<IActionResult> UpdatePatch(int id, JsonPatchDocument product)
        {
            await _productService.UpdatePatchAsync(id, product);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            await _productService.RemoveAsync(product);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

       
    }
}
