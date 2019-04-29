using Eventures.Data;
using Eventures.Models;
using Eventures.Services.Contracts;
using Eventures.ViewModels.Order;
using System;
using System.Linq;

namespace Eventures.Services
{
    public class OrderService : IOrderService
    {
        private readonly EventuresDbContext context;

        public OrderService(EventuresDbContext context)
        {
            this.context = context;
        }

        public OrderViewModel[] All()
        {
            return context.Orders
                .Select(o => new OrderViewModel
                {
                    Event = o.Event.Name,
                    Customer = o.Customer.UserName,
                    OrderedOn = o.OrderedOn
                })
            .ToArray();
        }

        public void CreateOrder(string eventId, int ticketsCount, string userId)
        {
            Event @event = context.Events.Find(eventId);
            if (@event == null || ticketsCount > @event.TotalTickets)
            {
                throw new ArgumentException("There is no enough tickets for this event!");
            }

            Order order = new Order
            {
                TicketsCount = ticketsCount,
                CustomerId = userId,
                EventId = eventId
            };

            context.Orders.Add(order);
            @event.TotalTickets -= order.TicketsCount;
            context.SaveChanges();
        }
    }
}
