using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FunApp.Data.Models;
using FunApp.Services.DataServices.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FunApp.Web.Pages.Categories
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly ICategoriesService categoriesService;

        public EditModel(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        [BindProperty]
        public int Id { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [MinLength(5)]
            public string Name { get; set; }
        }

        public void OnGet(int id)
        {
            this.Id = id;
            Category category = this.categoriesService.GetById(this.Id);
            this.Input = new InputModel { Name = category.Name };
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await this.categoriesService.Edit(Input.Name,  this.Id);
            return RedirectToAction("All", "Category");
        }
    }
}