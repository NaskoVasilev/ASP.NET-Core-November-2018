using Eventures.Models;
using Eventures.ViewModels.Event;
using Microsoft.EntityFrameworkCore;

namespace Eventures.Services.Contracts
{
    public interface IEventService
    {
        void Add(EventViewModel product);

        EventAllViewModel[] All();

        UserEventViewModel[] UserEvents(string userId);
    }
}
