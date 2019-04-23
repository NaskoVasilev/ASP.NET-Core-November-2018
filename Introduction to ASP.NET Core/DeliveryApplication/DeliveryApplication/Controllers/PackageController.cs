using DeliveryApplication.Data.Models.Enums;
using DeliveryApplication.Services;
using DeliveryApplication.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApplication.Controllers
{
    public class PackageController : Controller
    {
        private readonly PackageService packageService;

        public PackageController(PackageService packageService)
        {
            this.packageService = packageService;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["Users"] = packageService.GetAllRecipients();
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(PackageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Users"] = packageService.GetAllRecipients();
                return View(model);
            }


            packageService.Create(model);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult Details(int id)
        {
            var model = packageService.GetById(id);
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Pending()
        {
            var packages = packageService.PackagesByStatus(Status.Pending);
            return View(packages);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Shipped()
        {
            var packages = packageService.PackagesByStatus(Status.Shipped);
            return View(packages);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delivered()
        {
            var packages = packageService.PackagesByStatus(Status.Delivered);
            return View(packages);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Ship(int id)
        {
            packageService.SetDeliveyDate(id);
            packageService.UpdateStatus(id, Status.Shipped);
            return RedirectToAction(nameof(Pending));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Deliver(int id)
        {
            packageService.UpdateStatus(id, Status.Delivered);
            return RedirectToAction(nameof(Shipped));
        }

        [Authorize]
        public IActionResult Acquire(int id, int recipientId)
        {
            packageService.UpdateStatus(id, Status.Acquired);
            packageService.GenerateReceipt(id, recipientId);
            return RedirectToAction("Index", "Home");
        }
    }
}