using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class OrderDto
    {
        [Required]
        public string BasketId { get; set; }

        public int ShippingId { get; set; }

        public AddressDto ShippingAddress { get; set; }
    }
}