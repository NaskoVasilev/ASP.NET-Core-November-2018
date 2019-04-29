using Eventures.Models;
using Eventures.Services.Contracts;
using Eventures.ViewModels.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using X.PagedList;

namespace Eventures.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService service;
        private readonly UserManager<User> userManager;
        private const int PageSize = 6;

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
        public IActionResult All(int? page)
        {
            OrderViewModel[] orders = service.All();
            int pageNumber = page ?? 1;
            var pagedOrders = orders.ToPagedList(pageNumber, PageSize);
            return View(pagedOrders);
        }
    }
}