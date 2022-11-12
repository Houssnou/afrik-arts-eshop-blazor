using System.ComponentModel.DataAnnotations;
using Core.Entities;

namespace API.Dtos
{
    public class ProductToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public string ProductType { get; set; }
        public string ProductOrigin { get; set; }
    }

    public class TypeForReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class TypeDto
    {
        [Required]
        public string Name { get; set; }
    }

    public class OriginForReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class OriginDto
    {
        [Required]
        public string Name { get; set; }
    }

    public class ProductDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Type { get; set; }
        [Required]
        public int ProductOrigin { get; set; }
    }
}