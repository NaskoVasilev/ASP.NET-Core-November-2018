using DeliveryApplication.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DeliveryApplication.Components
{
    [ViewComponent(Name = "PackagesByStatus")]
    public class PackagesByStatusViewComponent : ViewComponent
    {
        private readonly PackageService service;
        private readonly UserManager<IdentityUser> userManager;

        public PackagesByStatusViewComponent(PackageService service, UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
            this.service = service;
        }

        public async Task<IViewComponentResult> InvokeAsync(string status)
        {
            var user = this.User as ClaimsPrincipal;
            var userId = userManager.GetUserId(user);
            ViewData["action"] = status.ToLower();
            var packages = service.GetPackagesByStatus(status, userId);
            return View(packages);
        }
    }
}
