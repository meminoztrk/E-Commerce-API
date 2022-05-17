using AutoMapper;
using NLayer.Core;
using NLayer.Core.DTOs;
using NLayer.Core.DTOs.CategoryDTOs;
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
        public async Task<CustomResponseDto<List<CategoryWithName>>> GetNavCategories(int id)
        {
            List<Category> nav = new List<Category>();
            while (true)
            {
                var Category =  _categoryRepository.Where(x => x.Id == id).FirstOrDefault();
                id=Category.SubId;
                nav.Add(Category);
                if(Category.Id == Category.SubId)
                {
                    break;
                } 
            }

            var categoryNav = _mapper.Map<List<CategoryWithName>>(nav.OrderBy(x=>x.Id).ToList());
            return CustomResponseDto<List<CategoryWithName>>.Success(200, categoryNav);
        }
        public async Task<CustomResponseDto<List<CategoryWithSubCount>>> GetSubCategoriesWithIdAsync(int id)
        {
            var SubCategories = await _categoryRepository.GetSubCategoriesWithIdAsync(id);
            var categoryWithSubCountDto = _mapper.Map<List<CategoryWithSubCount>>(SubCategories.ToList());
            foreach (var item in categoryWithSubCountDto)
            {
                item.SubCount = _categoryRepository.Where(x => x.SubId == item.Id && x.Id != x.SubId).ToList().Count();
            }
            return CustomResponseDto<List<CategoryWithSubCount>>.Success(200, categoryWithSubCountDto);
        }

        public async Task<CustomResponseDto<List<CategoryWithSubCount>>> GetUnDeletedCategoriesAsync()
        {
            var mainCategory = await _categoryRepository.GetAllMainCategoryAsync();
            var categoryWithSubCountDto = _mapper.Map<List<CategoryWithSubCount>>(mainCategory.ToList());
            foreach(var item in categoryWithSubCountDto)
            {
                item.SubCount = _categoryRepository.Where(x=>x.SubId == item.Id && x.Id != x.SubId).ToList().Count();
            }
            return CustomResponseDto<List<CategoryWithSubCount>>.Success(200, categoryWithSubCountDto);
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
