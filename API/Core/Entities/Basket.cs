using System.Collections.Generic;

namespace Core.Entities
{
    public class Basket
    {
        public Basket()
        {
        }

        public Basket(string id)
        {
            Id = id;
        }

       public string Id { get; set; }
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();

        public int? ShippingId { get; set; }
       public string ClientSecret { get; set; }
       public string InvoiceId { get; set; }
        public decimal shippingPrice { get; set; }
    }
}