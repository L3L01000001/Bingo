using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _db;
        public ProductRepository(StoreContext db)
        {
            _db = db;

        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _db.Products
             .Include(p => p.Brand)
            .Include(p => p.SubCategory)
            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await _db.Products
            .Include(p => p.Brand)
            .Include(p => p.SubCategory)
            .ToListAsync();
        }

        public async Task<IReadOnlyList<SubCategory>> GetProductSubCategoriesAsync()
        {
            return await _db.SubCategories
            .Include(s => s.Category)
            .ToListAsync();
        }
        public async Task<IReadOnlyList<Brand>> GetProductBrandsAsync()
        {
            return await _db.Brands.ToListAsync();
        }

    }
}