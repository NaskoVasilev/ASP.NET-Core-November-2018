using DeliveryApplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApplication.Controllers
{
    public class ReceiptController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ReceiptService service;

        public ReceiptController(UserManager<IdentityUser> userManager, ReceiptService service)
        {
            this.userManager = userManager;
            this.service = service;
        }

        [Authorize]
        public IActionResult UserReceipts()
        {
            string id = userManager.GetUserId(this.User);
            var receipts = service.GetRecepitsById(id);
            return View(receipts);
        }

        [Authorize]
        public IActionResult Details(int id)
        {
            var receipt = service.GetReceiptById(id);
            return View(receipt);
        }
    }
}