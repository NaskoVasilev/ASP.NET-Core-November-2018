using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Eventures.Services.Contracts;
using Eventures.ViewModels.Event;
using Microsoft.Extensions.Logging;
using Eventures.Filters;
using Microsoft.AspNetCore.Identity;
using Eventures.Models;
using System.Threading.Tasks;

namespace Eventures.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService eventService;
        private readonly ILogger<EventController> logger;
        private readonly UserManager<User> userManager;

        public EventController(IEventService eventService, ILogger<EventController> logger, UserManager<User> userManager)
        {
            this.eventService = eventService;
            this.logger = logger;
            this.userManager = userManager;
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }
        
        [TypeFilter(typeof(AdminTrackActionFilter))]
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public IActionResult Create(EventViewModel model)
        {
            if (!ModelState.IsValid && model.Start < model.End)
            {
                return View(model);
            }

            eventService.Add(model);
            logger.LogInformation("Event created: " + model.Name, model);
            return RedirectToAction("All");
        }

        [Authorize]
        public IActionResult All()
        {
            EventAllViewModel[] events = eventService.All();
            return View(events);
        }

        [Authorize]
        public IActionResult MyEvents()
        {
            string userId = userManager.GetUserId(this.User);
            UserEventViewModel[] userEvents = eventService.UserEvents(userId);
            return View(userEvents);
        }
    }
}