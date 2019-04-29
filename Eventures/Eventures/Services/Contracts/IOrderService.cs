using Eventures.ViewModels.Order;

namespace Eventures.Services.Contracts
{
    public interface IOrderService
    {
        void CreateOrder(string eventId, int ticketsCount, string userId);

        OrderViewModel[] All();
    }
}
