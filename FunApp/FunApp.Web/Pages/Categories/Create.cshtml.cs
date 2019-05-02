using FunApp.Services.DataServices.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace FunApp.Web.Pages.Categories
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ICategoriesService categoriesService;

        public CreateModel(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [MinLength(3)]
            public string Name { get; set; }
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await categoriesService.Create(Input.Name);
            return RedirectToAction("All", "Category");
        }
    }
}