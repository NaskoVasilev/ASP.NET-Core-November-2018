using FunApp.Services.DataServices.Contracts;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace FunApp.Web.Controllers
{
    public class CategoryController : Controller
    {
        private const int PageSize = 20;
        private readonly ICategoriesService categoriesService;

        public CategoryController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public IActionResult All(int? page)
        {
            var categories = categoriesService.GetAll();
            int pageNumber = page ?? 1;
            var pagedCategories = categories.ToPagedList(pageNumber, PageSize);
            return View(pagedCategories);
        }
    }
}