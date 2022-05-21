using AutoMapper;
using NLayer.Core.DTOs;
using NLayer.Core.DTOs.BrandDTOs;
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
    public class BrandService : Service<Brand>, IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public BrandService(IGenericRepository<Brand> repository,IMapper mapper, IBrandRepository brandRepository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _brandRepository = brandRepository;
        }

        public async Task<CustomResponseDto<List<BrandDto>>> GetUndeletedBrandAsync()
        {
            var brands = await _brandRepository.GetUndeletedBrandAsync();
            var brandDto = _mapper.Map<List<BrandDto>>(brands);
            return CustomResponseDto<List<BrandDto>>.Success(200,brandDto);
        }
    }
}
