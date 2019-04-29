using Eventures.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Eventures.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            ErrorViewModel errorView = new ErrorViewModel()
            {
                ErrorMessage = "Some error occured. Please try again!"
            };
            return View(errorView);
        }
    }
}
