using NLayer.Core;
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
    public class ProductFeatureService : Service<ProductFeature>, IProductFeatureService
    {
        public ProductFeatureService(IGenericRepository<ProductFeature> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
