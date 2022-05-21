using NLayer.Core.DTOs;
using NLayer.Core.DTOs.BrandDTOs;
using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Services
{
    public interface IBrandService : IService<Brand>
    {
        Task<CustomResponseDto<List<BrandDto>>> GetUndeletedBrandAsync();
    }
}
