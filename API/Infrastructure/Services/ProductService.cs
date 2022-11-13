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
using FluentResults;

namespace Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IBaseRepository<Product> _productsRepo;
        private readonly IBaseRepository<ProductOrigin> _productBrandRepo;
        private readonly IBaseRepository<ProductType> _productTypeRepo;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IBaseRepository<Product> productsRepo,
            IBaseRepository<ProductOrigin> productBrandRepo,
            IBaseRepository<ProductType> productTypeRepo, IUnitOfWork unitOfWork)
        {
            _productsRepo = productsRepo;
            _productBrandRepo = productBrandRepo;
            _productTypeRepo = productTypeRepo;
            _unitOfWork = unitOfWork;
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

        public async Task<Result<ProductType>> AddProductTypeAsync(ProductType productType)
        {
            var type = await _unitOfWork.Repository<ProductType>()
                .GetEntityWithSpec(new ProductTypeByNameSpecification(productType.Name));

            if (type != null)
                return Result.Fail($"""Product Type: '{productType.Name.ToLower()} ' exists already.""");

            _unitOfWork.Repository<ProductType>().Add(productType);
            throw new ArgumentException($"""Error while saving product type: '{productType.Name.ToLower()} '.""");
            await _unitOfWork.Complete();

            return Result.Ok(productType);
        }
    }
}
