using System.ComponentModel.DataAnnotations;

namespace Admin.Data.ProductType;

public class ProductType
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
}