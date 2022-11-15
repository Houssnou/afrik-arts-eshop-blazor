using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndOriginsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndOriginsSpecification(ProductSpecParams productParams)
        : base(p =>
            (string.IsNullOrEmpty(productParams.Search) || p.Name.ToLower().Contains(productParams.Search)) &&
            (!productParams.OriginId.HasValue || p.ProductOriginId == productParams.OriginId) &&
            (!productParams.TypeId.HasValue || p.ProductTypeId == productParams.TypeId)
        )
        {
            AddInclude(p => p.ProductOrigin);
            AddInclude(p => p.ProductType);

            AddOrderBy(x => x.Name);

            ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);

            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
        }

        public ProductsWithTypesAndOriginsSpecification(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductOrigin);
            AddInclude(p => p.ProductType);
        }

        public ProductsWithTypesAndOriginsSpecification() : base()
        {
            AddInclude(p => p.ProductOrigin);
            AddInclude(p => p.ProductType);
        }
    }


}