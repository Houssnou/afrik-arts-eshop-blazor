using Core.Entities;

namespace Core.Specifications;

public class ProductTypeByNameSpecification : BaseSpecification<ProductType>
{
    public ProductTypeByNameSpecification(string name) : base(t => t.Name.ToLower() == name.ToLower())
    {
    }
}