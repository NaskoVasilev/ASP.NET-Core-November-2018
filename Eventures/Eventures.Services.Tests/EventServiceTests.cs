using AutoMapper;
using Eventures.Data;
using Eventures.MappingConfiguration;
using Eventures.Models;
using Eventures.Services.Contracts;
using Eventures.ViewModels.Event;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Eventures.Services.Tests
{
    public class EventServiceTests
    {
        [Fact]
        public void AddShoudAddTheEventToTheDatabaseWithCorrectData()
        {
            using (var context = GetContext("AddEvent"))
            {
                IMapper mapper = GetAutoMapper();
                IEventService eventService = new EventService(context, mapper);
                EventViewModel eventView = new EventViewModel()
                {
                    Name = "Test",
                    Start = DateTime.Now.AddDays(1),
                    End = DateTime.Now.AddDays(2),
                    TotalTickets = 100,
                    PricePerTicket = 10,
                    Place = "Test place"
                };

                eventService.Add(eventView);
                Assert.Equal(1, context.Events.Count());
                Event @event = context.Events.First();
                Assert.Equal(@event.Name, eventView.Name);
                Assert.Equal(@event.PricePerTicket, eventView.PricePerTicket);
                Assert.Equal(@event.TotalTickets, eventView.TotalTickets);
            }
        }

        [Fact]
        public void AllReturnsAllEventWithPositiveAmmountOfTicktest()
        {
            using (var context = GetContext("AllEvnets"))
            {
                context.Events.AddRange(GetTestData());
                context.SaveChanges();

                IMapper mapper = GetAutoMapper();
                IEventService eventService = new EventService(context, mapper);
                var events = eventService.All();
                Assert.Equal(2, events.Length);
            }
        }

        [Fact]
        public void UserEventsReturnsAllUserEvents()
        {
            using (var context = GetContext("UserEvents"))
            {
                context.Orders.AddRange(GeTestOrders());
                context.SaveChanges();

                IMapper mapper = GetAutoMapper();
                IEventService eventService = new EventService(context, mapper);
                var events = eventService.UserEvents("1");
                Assert.Single(events);
                Assert.Equal("Test", events[0].Name);
            }
        }

        private EventuresDbContext GetContext(string databaseName)
        {
            var options = new DbContextOptionsBuilder<EventuresDbContext>()
               .UseInMemoryDatabase(databaseName)
               .Options;

            return new EventuresDbContext(options);
        }

        private IMapper GetAutoMapper()
        {
            IMapper mapper = null;
            try
            {
                mapper = Mapper.Instance;
            }
            catch (Exception)
            {
                Mapper.Initialize(config =>
                {
                    config.AddProfile<EventuresProfile>();
                });
                mapper = Mapper.Instance;
            }

            return mapper;
        }

        private IEnumerable<Order> GeTestOrders()
        {
            var events = GetTestData().ToArray();
            return new List<Order>()
            {
                new Order()
                {
                    Customer = new User()
                    {
                        Id = "1",
                        FirstName = "Atanas",
                        LastName = "Vasilev",
                        UserName = "Nasko",
                    },
                    Event = events[0],
                    OrderedOn = DateTime.Now,
                    TicketsCount = 10
                },
                new Order()
                {
                    Customer = new User()
                    {
                        Id = "2",
                        FirstName = "Other",
                        LastName = "Other",
                        UserName = "Other",
                    },
                    Event = events[1],
                    OrderedOn = DateTime.Now,
                    TicketsCount = 20
                }
            };
        }

        private IEnumerable<Event> GetTestData()
        {
            return new List<Event>()
            {
                new Event()
                {
                    Name = "Test",
                    Start = DateTime.Now.AddDays(1),
                    End = DateTime.Now.AddDays(2),
                    TotalTickets = 100,
                    PricePerTicket = 10,
                    Place = "Test place"
                },
                new Event()
                {
                    Name = "Test1",
                    Start = DateTime.Now.AddDays(2),
                    End = DateTime.Now.AddDays(5),
                    TotalTickets = 500,
                    PricePerTicket = 20,
                    Place = "Tes1 place"
                },
                new Event()
                {
                    Name = "Tes2",
                    Start = DateTime.Now,
                    End = DateTime.Now.AddDays(1),
                    TotalTickets = 0,
                    PricePerTicket = 10,
                    Place = "Test2 place"
                }
            };
        }
    }
}
