using System.ComponentModel.DataAnnotations;

namespace Admin.Data.Product;

public class ProductForCreation
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }

    [Required] public decimal Price { get; set; } = 0.00M;
    [Required] public string PictureUrl { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "Please provide a valid product type.")]
    public int Type { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "Please provide a valid product origin.")]
    public int Origin { get; set; }
}