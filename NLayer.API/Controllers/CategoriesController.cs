using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NLayer.API.Filters;
using NLayer.Core;
using NLayer.Core.DTOs;
using NLayer.Core.DTOs.CategoryDTOs;
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

        [HttpPatch("{id}")]

        public async Task<IActionResult> UpdatePatch(int id, JsonPatchDocument category)
        {
            await _categoryService.UpdatePatchAsync(id,category);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetCategories()
        {
            return CreateActionResult(await _categoryService.GetUnDeletedCategoriesAsync());
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetSubCategoriesWithId(int id)
        {
            return CreateActionResult(await _categoryService.GetSubCategoriesWithIdAsync(id));
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
        public async Task<IActionResult> Save(CategoryPostDto categoryPostDto)
        {
            var category = await _categoryService.AddAsync(_mapper.Map<Category>(categoryPostDto));

            var lastCategory = await _categoryService.GetByIdAsync(category.Id);
            if(categoryPostDto.SubId == 0)
            {
                lastCategory.SubId = lastCategory.Id;
                await _categoryService.UpdateAsync(lastCategory);
            }
            var categoryDtos = _mapper.Map<CategoryPostDto>(lastCategory);

            return CreateActionResult(CustomResponseDto<CategoryPostDto>.Success(201, categoryDtos));
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
