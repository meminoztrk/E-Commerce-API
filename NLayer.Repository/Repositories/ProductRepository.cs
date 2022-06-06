using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NLayer.Core;
using NLayer.Core.DTOs.ProductDTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProductRepository(AppDbContext context, IHttpContextAccessor httpContextAccessor) : base(context)
        {
           _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<CategoryFeature>> GetCategoryFeaturesByCategoryId(int id)
        {
            return await _context.CategoryFeatures.Where(x=>x.CategoryId == id && x.IsActive == true && x.IsDeleted == false).ToListAsync();
        }

        public async Task<List<Product>> GetProductWithCategory()
        {
            return await _context.Products.Include(x=> x.Category).ToListAsync();
        }
        public async Task<List<Product>> GetUndeletedProductAsync()
        {
            return await _context.Products.Include(x => x.Category).Include(x => x.Brand).Where(x => x.IsDeleted == false).ToListAsync();
        }
        public async Task<List<ProductIListDto>> GetProductWithFeaturesByCategoryId(int id)
        {
            var request = _httpContextAccessor.HttpContext.Request;
            return await _context.Products.Include(x => x.ProductImages).Include(x => x.ProductFeatures).Where(x => x.CategoryId == id && x.IsActive == true && x.IsDeleted == false).Select(x => new ProductIListDto
            {
                Id = x.Id,
                Name = x.Name,
                Image = request.Scheme + "://" + request.Host.Value + "/img/product/" + x.ProductImages.FirstOrDefault().Path,
                Price = x.ProductFeatures.FirstOrDefault().FePrice
            }).ToListAsync();
        }

    }
}
