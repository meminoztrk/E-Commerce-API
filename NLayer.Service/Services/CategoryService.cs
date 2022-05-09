using AutoMapper;
using NLayer.Core;
using NLayer.Core.DTOs;
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
    public class CategoryService : Service<Category>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(IGenericRepository<Category> repository, IUnitOfWork unitOfWork, IMapper mapper, ICategoryRepository categoryRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<CustomResponseDto<List<CategoryWithSubsDto>>> GetCategoryWithSubAsync()
        {
            var mainCategories = await _categoryRepository.GetAllMainCategoryAsync();
            var subCategories = await _categoryRepository.GetCategoryWithSubAsync();

            List<CategoryWithSubsDto> subs = new List<CategoryWithSubsDto>();
            foreach (var mainCategory in mainCategories)
            {
                List<CategoryDtoWithSubDto> subCats = new List<CategoryDtoWithSubDto>();
                var selectedSubCategories = subCategories.Where(x => x.SubId == mainCategory.Id).ToList();
                foreach(var subCategory in selectedSubCategories)
                {
                    List<CategoryDto> lastCats = new List<CategoryDto>();
                    var selectedLastCategories = subCategories.Where(x => x.SubId == subCategory.Id).ToList();
                    foreach(var lastCategory in selectedLastCategories)
                    {
                        lastCats.Add(new CategoryDto { Id = lastCategory.Id, Name = lastCategory.Name });
                    }
                    subCats.Add(new CategoryDtoWithSubDto { Id = subCategory.Id, Name = subCategory.Name, SubCategories = lastCats });

                }
                subs.Add(new CategoryWithSubsDto { Id = mainCategory.Id, Name = mainCategory.Name, SubCategories = subCats });
            }

            return CustomResponseDto<List<CategoryWithSubsDto>>.Success(200, subs);
        }

        public async Task<CustomResponseDto<List<CategoryDto>>> GetAllMainCategoryAsync()
        {
            var mainCategory = await _categoryRepository.GetAllMainCategoryAsync();
            var categoryDto = _mapper.Map<List<CategoryDto>>(mainCategory.ToList());
            return CustomResponseDto<List<CategoryDto>>.Success(200, categoryDto);
        }    

        public async Task<CustomResponseDto<CategoryWithProductsDto>> GetSingleCategoryByIdWithProductsAsync(int categoryId)
        {
            var category = await _categoryRepository.GetSingleCategoryByIdWithProductsAsync(categoryId);

            var categoryDto = _mapper.Map<CategoryWithProductsDto>(category);

            return CustomResponseDto<CategoryWithProductsDto>.Success(200, categoryDto);
        }
    }
}
