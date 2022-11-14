using Core.Entities;

namespace Core.Specifications;

public class ProductOriginByNameSpecification : BaseSpecification<ProductOrigin>
{
    public ProductOriginByNameSpecification(string name) : base(t => t.Name.ToLower() == name.ToLower())
    {
    }
}