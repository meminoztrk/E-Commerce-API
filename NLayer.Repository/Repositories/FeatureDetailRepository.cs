using NLayer.Core.Models;
using NLayer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Repositories
{
    internal class FeatureDetailRepository : GenericRepository<FeatureDetail>, IFeatureDetailRepository
    {
        public FeatureDetailRepository(AppDbContext context) : base(context)
        {
        }
    }
}
