using AutoMapper;
using NLayer.Core.DTOs;
using NLayer.Core.DTOs.CategoryDTOs;
using NLayer.Core.DTOs.FeatureDTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;


namespace NLayer.Service.Services
{
    public class FeatureService : Service<CategoryFeature>, IFeatureService
    {
        private readonly IFeatureRepository _featureRepository;
        private readonly IMapper _mapper;
        public FeatureService(IGenericRepository<CategoryFeature> repository,IFeatureRepository featureRepository, IMapper mapper, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
            _featureRepository = featureRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<List<CategoryWithName>>> GetLastCategoriesAsync()
        {
            var LastCategories = await _featureRepository.GetLastCategoriesAsync();
            return CustomResponseDto<List<CategoryWithName>>.Success(200, LastCategories);
        }

        public async Task<CustomResponseDto<List<CategoryFeatureDto>>> GetUndeletedCategoryFeatureAsync()
        {
            var Features = await _featureRepository.GetUndeletedCategoryFeatureAsync();
            var FeatureDto = _mapper.Map<List<CategoryFeatureDto>>(Features);

            return CustomResponseDto<List<CategoryFeatureDto>>.Success(200, FeatureDto);
        }
    }
}
