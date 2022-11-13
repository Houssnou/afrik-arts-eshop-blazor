using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specifications;
using FluentResults;

namespace Core.Interfaces;

public interface IProductService
{
    Task<IReadOnlyList<Product>> GetProductsAsync(ProductSpecParams productParams);
    Task<int> CountProductsAsync(ProductSpecParams productParams);
    Task<Product> GetProductAsyncById(int id);
    Task<Product> AddProductAsync(Product product);
    Task<Product> UpdateProductAsync(int id, Product product);
    Task DeleteProductAsync(int id);

    Task<IReadOnlyList<ProductOrigin>> GetProductOriginsAsync();
    Task<ProductOrigin> AddProductOriginAsync(ProductOrigin productOrigin);

    Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
    Task<Result<ProductType>> AddProductTypeAsync(ProductType productType);
}