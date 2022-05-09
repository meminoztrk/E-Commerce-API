using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.API.Filters;
using NLayer.Core;
using NLayer.Core.DTOs;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    public class CategoriesController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCategoryWithSub()
        {
            return CreateActionResult(await _categoryService.GetCategoryWithSubAsync());
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllMainCategory()
        {
            return CreateActionResult(await _categoryService.GetAllMainCategoryAsync());
        }

        [HttpGet("[action]/{categoryId}")]
        public async  Task<IActionResult> GetSingleCategoryByIdWithProducts(int categoryId)
        {
            return CreateActionResult(await _categoryService.GetSingleCategoryByIdWithProductsAsync(categoryId));
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var categories = await _categoryService.GetAllAsync();
            var categoryDtos = _mapper.Map<List<CategoryDto>>(categories.ToList());

            return CreateActionResult(CustomResponseDto<List<CategoryDto>>.Success(200, categoryDtos));
        }

        [ServiceFilter(typeof(NotFoundFilter<Category>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            var categoryDto = _mapper.Map<CategoryDto>(category);

            return CreateActionResult(CustomResponseDto<CategoryDto>.Success(200, categoryDto));
        }

        [HttpPost]
        public async Task<IActionResult> Save(CategoryDto categoryDto)
        {
            var category = await _categoryService.AddAsync(_mapper.Map<Category>(categoryDto));
            var categoryDtos = _mapper.Map<CategoryDto>(category);

            return CreateActionResult(CustomResponseDto<CategoryDto>.Success(201, categoryDtos));
        }

        [HttpPut]
        public async Task<IActionResult> Update(CategoryUpdateDto categoryDto)
        {
            await _categoryService.UpdateAsync(_mapper.Map<Category>(categoryDto));

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            await _categoryService.RemoveAsync(category);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
