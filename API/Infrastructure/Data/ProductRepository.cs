using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreDbContext _context;


        public ProductRepository(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<ProductOrigin>> GetProductBrandsAsync()
        {
            return await _context.ProductOrigins.ToArrayAsync();
        }

        public async Task<Product> GetProductbyIdAsync(int id)
        {
            return await _context.Products
                 .Include(p => p.ProductOrigin)
                 .Include(p => p.ProductType)
                 .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await _context.Products
                 .Include(p => p.ProductOrigin)
                 .Include(p => p.ProductType)
                 .ToListAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            return await _context.ProductTypes.ToListAsync();
        }
    }
}