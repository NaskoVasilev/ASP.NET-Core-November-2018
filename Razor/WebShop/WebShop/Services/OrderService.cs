using System.Globalization;
using System.Linq;
using WebShop.Data;
using WebShop.Models;
using WebShop.Services.Contracts;
using WebShop.ViewModels.Order;

namespace WebShop.Services
{
    public class OrderService : IOrderService
    {
        private readonly WebShopDbContext context;

        public OrderService(WebShopDbContext context)
        {
            this.context = context;
        }

        public OrderViewModel[] All()
        {
            var orders = context.Orders
                .Select(o => new OrderViewModel()
                {
                    Id = o.Id,
                    Product = o.Product.Name,
                    OrderedOn = o.OrderOn.ToString("HH:mm dd/MM/yyyy", CultureInfo.InvariantCulture),
                    Customer = o.Client.UserName
                })
                .ToArray();

            return orders;
        }
    }
}
