using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class BasketDto
    {
        [Required]
        public string Id { get; set; }
        public List<BasketItemDto> Items { get; set; }
        public int? ShippingId { get; set; }
        public string ClientSecret { get; set; }
        public string InvoiceId { get; set; }
        public decimal shippingPrice { get; set; }

    }
}