using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShop.Services;
using WebShop.Services.Contracts;
using WebShop.ViewModels.Order;

namespace WebShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService service;

        public OrderController(IOrderService service)
        {
            this.service = service;
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult All()
        {
            OrderViewModel[] orders = service.All();
            return View(orders);
        }
    }
}