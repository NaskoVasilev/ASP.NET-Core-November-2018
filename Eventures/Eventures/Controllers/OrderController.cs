using Eventures.Models;
using Eventures.Services.Contracts;
using Eventures.ViewModels.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Eventures.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService service;
        private readonly UserManager<User> userManager;

        public OrderController(IOrderService service, UserManager<User> manager)
        {
            this.service = service;
            this.userManager = manager;
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(string eventId, int ticketsCount)
        {
            string userId = userManager.GetUserId(this.User);
            try
            {
                service.CreateOrder(eventId, ticketsCount, userId);
            }
            catch (ArgumentException ex)
            {
                string message = ex.Message;
                TempData["Error"] = message;
                return RedirectToAction("All", "Event");
            }

            return RedirectToAction("All", "Event");
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult All()
        {
            OrderViewModel[] orders = service.All();
            return View(orders);
        }
    }
}