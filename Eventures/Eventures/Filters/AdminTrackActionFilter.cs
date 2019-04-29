using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;

namespace Eventures.Filters
{
    public class AdminTrackActionFilter : ActionFilterAttribute
    {
        private readonly ILogger<ActionFilterAttribute> logger;

        public AdminTrackActionFilter(ILogger<ActionFilterAttribute> logger)
        {
            this.logger = logger;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            string username = context.HttpContext.User.Identity.Name;
            string eventName = context.HttpContext.Request.Form["Name"];
            string end = context.HttpContext.Request.Form["End"];
            string start = context.HttpContext.Request.Form["Start"];

            logger.LogInformation($"{DateTime.Now} Administrator {username} create event {eventName} ({end} / {start})");

            base.OnActionExecuted(context);
        }
    }
}
