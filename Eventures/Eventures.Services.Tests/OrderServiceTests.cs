using Eventures.Data;
using Eventures.Models;
using Eventures.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Eventures.Services.Tests
{
    public class OrderServiceTests
    {
        [Fact]
        public void CreateOrderThrowsErrorWithInvalidEventId()
        {
            using (var context = GetContext("CreateOrder"))
            {
                context.Events.AddRange(GetEvents());
                context.Users.AddRange(GetUsers());
                context.SaveChanges();

                IOrderService orderService = new OrderService(context);
                Assert.Throws<ArgumentException>(() => orderService.CreateOrder("other", 10, "nasko"));
            }
        }

        [Fact]
        public void CreateOrderThrowsErrorWithInvalidTicketsCount()
        {
            using (var context = GetContext("CreateInvalidOrder"))
            {
                context.Events.AddRange(GetEvents());
                context.Users.AddRange(GetUsers());
                context.SaveChanges();

                IOrderService orderService = new OrderService(context);
                Assert.Throws<ArgumentException>(() => orderService.CreateOrder("1", 10, "nasko"));
            }
        }

        [Fact]
        public void CreateOrderShouldCreateOrderWithValidData()
        {
            using (var context = GetContext("CreateValidOrder"))
            {
                context.Events.AddRange(GetEvents());
                context.Users.AddRange(GetUsers());
                context.SaveChanges();

                IOrderService orderService = new OrderService(context);
                orderService.CreateOrder("2", 10, "nasko");
                Assert.Equal(1, context.Orders.Count());
                Order order = context.Orders.FirstOrDefault();
                Assert.Equal("2", order.EventId);
                Assert.Equal("nasko", order.CustomerId);
                Assert.Equal(10, order.TicketsCount);
            }
        }

        [Fact]
        public void CreateOrderShouldDecreaseEventTicketsCount()
        {
            using (var context = GetContext("CreateValidOrder"))
            {
                context.Events.AddRange(GetEvents());
                context.Users.AddRange(GetUsers());
                context.SaveChanges();

                IOrderService orderService = new OrderService(context);
                orderService.CreateOrder("2", 10, "nasko");
                Event @event = context.Events.FirstOrDefault(x => x.Id == "2");
                Assert.Equal(10, @event.TotalTickets);
            }
        }

        private EventuresDbContext GetContext(string databaseName)
        {
            var options = new DbContextOptionsBuilder<EventuresDbContext>()
               .UseInMemoryDatabase(databaseName)
               .Options;

            return new EventuresDbContext(options);
        }

        private IEnumerable<User> GetUsers()
        {
            return new List<User>()
            {
                new User()
                {
                    Id = "nasko",
                    UserName = "Nasko",
                },
                new User()
                {
                    Id = "pesho",
                    UserName = "Pesho",
                },
            };
        }

        private IEnumerable<Event> GetEvents()
        {
            return new List<Event>()
            {
                new Event()
                {
                    Id = "1",
                    Name = "test1",
                    PricePerTicket = 50,
                    TotalTickets = 5
                },
                new Event()
                {
                    Id = "2",
                    Name = "test2",
                    PricePerTicket = 10,
                    TotalTickets = 20
                },
                new Event()
                {
                    Id = "3",
                    Name = "test3",
                    PricePerTicket = 110,
                    TotalTickets = 200
                }
            };
        }
    }
}
