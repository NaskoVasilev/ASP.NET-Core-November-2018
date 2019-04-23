using WebShop.ViewModels.Order;

namespace WebShop.Services.Contracts
{
    public interface IOrderService
    {
        OrderViewModel[] All();
    }
}
