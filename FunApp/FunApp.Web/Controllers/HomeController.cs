using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FunApp.Web.Models;
using FunApp.Services.DataServices.Contracts;

namespace FunApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private const int HomePageJokesCount = 20;
        private readonly IJokesService jokesService;

        public HomeController(IJokesService jokesService)
        {
            this.jokesService = jokesService;
        }

        public IActionResult Index()
        {
            var jokes = jokesService.GetRandomJokes(HomePageJokesCount);
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
