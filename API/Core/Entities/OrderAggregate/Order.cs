using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities.OrderAggregate
{
    public class Order : BaseEntity
    {
        public Order()
        {
        }

        public Order(IReadOnlyList<OrderItem> orderItems, string buyerEmail, 
                     Address shippingAddress, Shipping shipping, decimal subtotal, string paymentId)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            Shipping = shipping;
            OrderItems = orderItems;
            Subtotal = subtotal;
            PaymentId = paymentId;
        }

       public string BuyerEmail { get; set; }

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        public Address ShippingAddress { get; set; }
        public Shipping Shipping { get; set; }

        public IReadOnlyList<OrderItem> OrderItems { get; set; }
        public decimal Subtotal { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

       public string PaymentId { get; set; }

        public decimal GetTotal() => Subtotal + Shipping.Price;
    }
}