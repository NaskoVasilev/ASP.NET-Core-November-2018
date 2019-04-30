using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FunApp.Services.DataServices.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace FunApp.Web.Controllers
{
    public class JokeController : Controller
    {
        private readonly IJokesService jokesService;

        public JokeController(IJokesService jokesService)
        {
            this.jokesService = jokesService;
        }

        public IActionResult Details(int id)
        {
            var joke = jokesService.GetById(id);
            return View(joke);
        }
    }
}