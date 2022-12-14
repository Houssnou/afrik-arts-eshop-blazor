using System.ComponentModel.DataAnnotations;

namespace Admin.Data.ProductType;

public class ProductType
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }

    public ProductType()
    { }
    public ProductType(int id, string name)
    {
        Id = id;
        Name = name;
    }
}