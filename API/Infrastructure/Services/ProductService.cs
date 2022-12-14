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

        public async Task<Result<Product>> GetProductAsyncById(int id)
        {
            var spec = new ProductsWithTypesAndOriginsSpecification(id);

            var product = await _productsRepo.GetEntityWithSpec(spec);

            return product == null ? Result.Fail($"""Product id: '{id} ' not found.""") : Result.Ok(product);
        }

        public async Task<Result<Product>> AddProductAsync(string name, string description, decimal price, string pictureUrl, int typeId, int originId)
        {
            var type = await _unitOfWork.Repository<ProductType>().GetByIdAsync(typeId);

            if (type == null) return Result.Fail($"""Invalid product type id: '{typeId} '""");

            var origin = await _unitOfWork.Repository<ProductOrigin>().GetByIdAsync(typeId);

            if (origin == null) return Result.Fail($"""Invalid product origin id: '{originId}'""");

            var product = new Product
            {
                Name = name,
                Description = description,
                Price = price,
                PictureUrl = pictureUrl,
                ProductTypeId = typeId,
                ProductType = type,
                ProductOriginId = originId,
                ProductOrigin = origin
            };

            _unitOfWork.Repository<Product>().Add(product);

            await _unitOfWork.Complete();

            return Result.Ok(product);
        }

        public async Task<Result<Product>> UpdateProductAsync(int id, string name, string description, decimal price, string pictureUrl, int pType, int pOrigin)
        {
            var product = await _unitOfWork.Repository<Product>().GetByIdAsync(id);

            if (product == null) return Result.Fail($"""Invalid product id: '{id}'""");

            var type = await _unitOfWork.Repository<ProductType>().GetByIdAsync(pType);

            if (type == null) return Result.Fail($"""Invalid product type id: '{pType} '""");

            var origin = await _unitOfWork.Repository<ProductOrigin>().GetByIdAsync(pOrigin);

            if (origin == null) return Result.Fail($"""Invalid product origin id: '{pOrigin}'""");

            product.Name = name;
            product.Description = description;
            product.Price = price;
            product.PictureUrl = pictureUrl;
            product.ProductTypeId = type.Id;
            product.ProductType = type;
            product.ProductOriginId = origin.Id;
            product.ProductOrigin = origin;

            _unitOfWork.Repository<Product>().Update(product);

            await _unitOfWork.Complete();

            return Result.Ok(product);
        }

        public async Task DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<ProductOrigin>> GetProductOriginsAsync()
        {
            return await _productBrandRepo.GetListAsync();
        }

        public async Task<Result<ProductOrigin>> AddProductOriginAsync(ProductOrigin productOrigin)
        {
            var type = await _unitOfWork.Repository<ProductOrigin>()
               .GetEntityWithSpec(new ProductOriginByNameSpecification(productOrigin.Name));

            if (type != null)
                return Result.Fail($"""Product Origin: '{productOrigin.Name.ToLower()} ' exists already.""");

            _unitOfWork.Repository<ProductOrigin>().Add(productOrigin);

            await _unitOfWork.Complete();

            return Result.Ok(productOrigin);
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

            await _unitOfWork.Complete();

            return Result.Ok(productType);
        }

        public async Task<IReadOnlyList<Product>> GetAllProductsAsync()
        {
            return await _productsRepo.ListAsync(new ProductsWithTypesAndOriginsSpecification());
        }
    }
}
