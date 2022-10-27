using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithFiltersForCountSpecification : BaseSpecification<Product>
    {
        public ProductsWithFiltersForCountSpecification(ProductSpecParams productParams)
        : base(p =>
            (string.IsNullOrEmpty(productParams.Search) || p.Name.ToLower().Contains(productParams.Search)) &&
            (!productParams.OriginId.HasValue || p.ProductOriginId == productParams.OriginId) &&
            (!productParams.TypeId.HasValue || p.ProductTypeId == productParams.TypeId)
        )
        {
        }
    }
}