using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Repositories
{
    public interface ICategoryRepository:IGenericRepository<Category>
    {
        Task<List<Category>> GetSubCategoriesWithIdAsync(int id);
        Task<Category> GetSingleCategoryByIdWithProductsAsync(int id);
        Task<List<Category>> GetAllMainCategoryAsync();
        Task<List<Category>> GetCategoryWithSubAsync();
    }
}
