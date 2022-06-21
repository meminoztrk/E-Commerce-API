using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NLayer.API.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.DTOs.BrandDTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    public class BrandsController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IBrandService _brandService;
        public BrandsController(IBrandService brandService, IMapper mapper)
        {
            _brandService = brandService;
            _mapper = mapper;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetBrands()
        {
            return CreateActionResult(await _brandService.GetUndeletedBrandAsync());
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var brands = await _brandService.GetAllAsync();
            var brandDtos = _mapper.Map<List<BrandDto>>(brands.ToList());

            return CreateActionResult(CustomResponseDto<List<BrandDto>>.Success(200, brandDtos));
        }

        //[ServiceFilter(typeof(NotFoundFilter<Brand>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var brand = await _brandService.GetByIdAsync(id);
            var brandDto = _mapper.Map<BrandDto>(brand);

            return CreateActionResult(CustomResponseDto<BrandDto>.Success(200, brandDto));
        }

        [HttpPost]
        public async Task<IActionResult> Save(BrandDto brandDto)
        {
            var brand = await _brandService.AddAsync(_mapper.Map<Brand>(brandDto));

            var brandDtos = _mapper.Map<BrandDto>(brand);

            return CreateActionResult(CustomResponseDto<BrandDto>.Success(201, brandDtos));
        }

        [HttpPut]
        public async Task<IActionResult> Update(BrandUpdateDto brandDto)
        {
            await _brandService.UpdateAsync(_mapper.Map<Brand>(brandDto));

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
        [HttpPatch("{id}")]

        public async Task<IActionResult> UpdatePatch(int id, JsonPatchDocument brand)
        {
            await _brandService.UpdatePatchAsync(id, brand);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var brand = await _brandService.GetByIdAsync(id);

            await _brandService.RemoveAsync(brand);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
