using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShop.Services.Contracts;
using WebShop.ViewModels.Product;

namespace WebShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public IActionResult Create(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            productService.Add(model);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult Details(int id)
        {
            var model = productService.GetById(id);
            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(int id)
        {
            ProductViewModel model = productService.GetById(id);
            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public IActionResult Edit(int id, ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            productService.UpdateProductById(id, model);
            return RedirectToAction("Details", new { id });
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Delete(int id)
        {
            ProductViewModel model = productService.GetById(id);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult DeletePost(int id)
        {
            productService.RemoveById(id);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult Order(int id)
        {
            productService.OrderProduct(id, this.User.Identity.Name);
            return RedirectToAction("Index", "Home");
        }
    }
}