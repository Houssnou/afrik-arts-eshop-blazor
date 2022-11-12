using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specifications;
using Stripe;
using Product = Core.Entities.Product;

namespace Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IBaseRepository<Product> _productsRepo;
        private readonly IBaseRepository<ProductOrigin> _productBrandRepo;
        private readonly IBaseRepository<ProductType> _productTypeRepo;

        public ProductService(IBaseRepository<Product> productsRepo,
            IBaseRepository<ProductOrigin> productBrandRepo,
            IBaseRepository<ProductType> productTypeRepo)
        {
            _productsRepo = productsRepo;
            _productBrandRepo = productBrandRepo;
            _productTypeRepo = productTypeRepo;
        }
        public async Task<IReadOnlyList<Product>> GetProductsAsync(ProductSpecParams productParams)
        {
            var productSpec = new ProductsWithTypesAndOriginsSpecification(productParams);

            return await _productsRepo.ListAsync(productSpec);
        }

        public async Task<int> CountProductsAsync(ProductSpecParams productParams)
        {
            var countSpec = new ProductsWithFiltersForCountSpecification(productParams);
            return await _productsRepo.CountAsync(countSpec);
        }

        public async Task<Product> GetProductAsyncById(int id)
        {
            var spec = new ProductsWithTypesAndOriginsSpecification(id);

            return await _productsRepo.GetEntityWithSpec(spec);
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> UpdateProductAsync(int id, Product product)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<ProductOrigin>> GetProductOriginsAsync()
        {
            return await _productBrandRepo.GetListAsync();
        }

        public async Task<ProductOrigin> AddProductOriginAsync(ProductOrigin productOrigin)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            return await _productTypeRepo.GetListAsync();
        }

        public async Task<ProductType> AddProductTypeAsync(ProductType productType)
        {
            throw new NotImplementedException();
        }
    }
}
