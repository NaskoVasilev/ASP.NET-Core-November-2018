using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FunApp.Web.Models;
using FunApp.Web.Models.Joke;
using FunApp.Data.Common;
using FunApp.Data.Models;

namespace FunApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Joke> jokeRepository;

        public HomeController(IRepository<Joke> jokeRepository)
        {
            this.jokeRepository = jokeRepository;
        }

        public IActionResult Index()
        {
            var jokes = jokeRepository.All()
                .OrderBy(x => Guid.NewGuid())
                .Select(j => new JokeIndexViewModel
                {
                    Content = j.Content,
                    CategoryName = j.Category.Name
                })
                .Take(10)
                .ToArray();

            return View(jokes);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
