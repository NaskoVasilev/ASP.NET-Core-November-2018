using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FunApp.Services.DataServices.Contracts;
using FunApp.Services.MachineLearning;
using FunApp.Web.Models.Jokes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace FunApp.Web.Controllers
{
    public class JokeController : Controller
    {
        private const int PageSize = 10;
        private const string TrainedModelPath = "MlModels/JokesCategoryModel.zip";
        private readonly IJokesService jokesService;
        private readonly ICategoriesService categoriesService;
        private readonly IJokesCategorizer jokesCategorizer;

        public JokeController(IJokesService jokesService, ICategoriesService categoriesService, IJokesCategorizer jokesCategorizer)
        {
            this.jokesService = jokesService;
            this.categoriesService = categoriesService;
            this.jokesCategorizer = jokesCategorizer;
        }

        public IActionResult Details(int id)
        {
            var joke = jokesService.GetById(id);
            return View(joke);
        }

        [Authorize]
        public IActionResult Create()
        {
            this.ViewData["Categories"] = this.categoriesService.GetAll()
                .Select(c => new SelectListItem
                {
                    Value = c.Id,
                    Text = c.NameAndCount
                })
                .ToList();

            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateJokeInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            int jokeId = await jokesService.Create(model.Content, model.CategoryId);
            return RedirectToAction(nameof(Details), new { id = jokeId });
        }

        [HttpPost]
        public SuggestCategoryResult SuggestCategory(string joke)
        {
            string normalizedJoke = jokesService.NormalizeJoke(joke);
            string category = jokesCategorizer.Categorize(TrainedModelPath, joke);
            int? categoryId = categoriesService.GetGategoryId(category);
            return new SuggestCategoryResult { CategoryId = categoryId ?? 0, CategoryName = category };
        }

        public IActionResult ByCategory(int categoryId, int? page)
        {
            var jokes = jokesService.JokesByCategory(categoryId);
            int pageNumber = page ?? 1;
            var pagedJokes = jokes.ToPagedList(pageNumber, PageSize);
            return View(pagedJokes);
        }

        public IActionResult All(int? page)
        {
            var jokes = jokesService.GetAll();
            int pageNumber = page ?? 1;
            var pagedJokes = jokes.ToPagedList(pageNumber, PageSize);
            return View(pagedJokes);
        }

        [HttpPost]
        public async Task<IActionResult> Rate([Range(1, 6)] int rating, int jokeId)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(new { Message = "The rating must be berween 1 and 6!" });
            }

            await jokesService.RateJoke(rating, jokeId);
            double newRating = jokesService.GetRate(jokeId);
            return Json(newRating.ToString("F2"));
        }
    }
}