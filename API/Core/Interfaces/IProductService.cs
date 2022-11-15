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
    Task<Result<Product>> GetProductAsyncById(int id);
    Task<Result<Product>> AddProductAsync(string name, string description, decimal price, string pictureUrl, int type, int origin);
    Task<Result<Product>> UpdateProductAsync(int id, string name, string description, decimal price, string pictureUrl, int type, int origin);
    Task DeleteProductAsync(int id);
    Task<IReadOnlyList<ProductOrigin>> GetProductOriginsAsync();
    Task<Result<ProductOrigin>> AddProductOriginAsync(ProductOrigin productOrigin);
    Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
    Task<Result<ProductType>> AddProductTypeAsync(ProductType productType);
    Task<IReadOnlyList<Product>> GetAllProductsAsync();
}