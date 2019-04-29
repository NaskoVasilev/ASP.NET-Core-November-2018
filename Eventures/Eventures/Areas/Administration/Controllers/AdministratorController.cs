using System.Linq;
using Eventures.Areas.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Eventures.Areas.Administration.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Area("Administration")]
    public class AdministratorController : Controller
    {
        private readonly IAdministrationService administrationService;

        public AdministratorController(IAdministrationService administrationService)
        {
            this.administrationService = administrationService;
        }

        public IActionResult AdminPanel()
        {
            var users = administrationService.GetAllUsers()
                .Where(u => u.UserName != this.User.Identity.Name)
                .ToArray();

            administrationService.SetIsAdminProperty(users);

            foreach (var user in users)
            {
            }

            return View(users);
        }

        public IActionResult Promote(string username)
        {
            administrationService.PromoteUser(username);
            return RedirectToAction(nameof(AdminPanel));
        }

        public IActionResult Demote(string username)
        {
            administrationService.DemoteUser(username);
            return RedirectToAction(nameof(AdminPanel));
        }
    }
}
