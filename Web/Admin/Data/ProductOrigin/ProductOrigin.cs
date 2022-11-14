using System.ComponentModel.DataAnnotations;

namespace Admin.Data.ProductOrigin;

public class ProductOrigin
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }

    public ProductOrigin()
    { }
    public ProductOrigin(int id, string name)
    {
        Id = id;
        Name = name;
    }
}