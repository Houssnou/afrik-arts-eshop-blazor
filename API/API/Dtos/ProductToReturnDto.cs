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

    public class ProductForListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
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
        public string PictureUrl { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please provide a valid product type Id.")]
        public int Type { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please provide a valid product origin Id.")]
        public int Origin { get; set; }

        public ProductDto()
        {
        }

        public ProductDto(int id, string name, string description, decimal price, string pictureUrl, int type, int origin)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            PictureUrl = pictureUrl;
            Type = type;
            Origin = origin;
        }
    }
}