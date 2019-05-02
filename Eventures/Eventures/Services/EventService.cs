using AutoMapper;
using AutoMapper.QueryableExtensions;
using Eventures.ViewModels.Event;
using System.Linq;
using Eventures.Data;
using Eventures.Models;
using Eventures.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;

namespace Eventures.Services
{
    public class EventService : IEventService
    {
        private readonly EventuresDbContext context;
        private readonly IMapper mapper;

        public EventService(EventuresDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public void Add(EventViewModel @event)
        {
            Event validEvent = mapper.Map<Event>(@event);
            context.Events.Add(validEvent);
            context.SaveChanges();
        }

        public EventAllViewModel[] All()
        {
            return context.Events
                .Where(e => e.TotalTickets > 0)
                .ProjectTo<EventAllViewModel>(mapper.ConfigurationProvider)
                .ToArray();
        }

        public UserEventViewModel[] UserEvents(string userId)
        {
            UserEventViewModel[] userEvents =  context.Orders
                .Include(o => o.Event)
                .Where(o => o.CustomerId == userId)
                .Select(o => new UserEventViewModel
                {
                    Name = o.Event.Name,
                    Start = o.Event.Start,
                    End = o.Event.End,
                    Tickets = o.TicketsCount
                })
                .ToArray();

            return userEvents;
        }
    }
}
