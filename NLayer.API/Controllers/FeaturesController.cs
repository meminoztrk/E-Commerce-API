using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NLayer.API.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.DTOs.FeatureDTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    public class FeaturesController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IFeatureService _featureService;
        public FeaturesController(IFeatureService featureService, IMapper mapper)
        {
            _featureService = featureService;
            _mapper = mapper;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetLastCategories()
        {
            return CreateActionResult(await _featureService.GetLastCategoriesAsync());
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetCategoryFeatures()
        {
            return CreateActionResult(await _featureService.GetUndeletedCategoryFeatureAsync());
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var CategoryFeatures = await _featureService.GetAllAsync();
            var CategoryFeatureDtos = _mapper.Map<List<CategoryFeatureDto>>(CategoryFeatures.ToList());

            return CreateActionResult(CustomResponseDto<List<CategoryFeatureDto>>.Success(200, CategoryFeatureDtos));
        }

        [ServiceFilter(typeof(NotFoundFilter<CategoryFeature>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var CategoryFeature = await _featureService.GetByIdAsync(id);
            var CategoryFeatureDto = _mapper.Map<CategoryFeatureDto>(CategoryFeature);

            return CreateActionResult(CustomResponseDto<CategoryFeatureDto>.Success(200, CategoryFeatureDto));
        }

        [HttpPost]
        public async Task<IActionResult> Save(CategoryFeatureDto CategoryFeatureDto)
        {
            var CategoryFeature = await _featureService.AddAsync(_mapper.Map<CategoryFeature>(CategoryFeatureDto));

            var CategoryFeatureDtos = _mapper.Map<CategoryFeatureDto>(CategoryFeature);

            return CreateActionResult(CustomResponseDto<CategoryFeatureDto>.Success(201, CategoryFeatureDtos));
        }

        [HttpPut]
        public async Task<IActionResult> Update(CategoryFeatureDto CategoryFeatureDto)
        {
            await _featureService.UpdateAsync(_mapper.Map<CategoryFeature>(CategoryFeatureDto));

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
        [HttpPatch("{id}")]

        public async Task<IActionResult> UpdatePatch(int id, JsonPatchDocument CategoryFeature)
        {
            await _featureService.UpdatePatchAsync(id, CategoryFeature);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var CategoryFeature = await _featureService.GetByIdAsync(id);

            await _featureService.RemoveAsync(CategoryFeature);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
