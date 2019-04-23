using System;

namespace WebShop.Models
{
    public class Order
    {
        public Order()
        {
            this.OrderOn = DateTime.Now;
        }

        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public string ClientId { get; set; }
        public User Client { get; set; }

        public DateTime OrderOn { get; set; } = DateTime.Now;
    }
}
