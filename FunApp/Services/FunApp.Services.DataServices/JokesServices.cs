using FunApp.Data.Common;
using FunApp.Data.Models;
using FunApp.Services.DataServices.Contracts;
using FunApp.Services.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using FunApp.Services.Mapping;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace FunApp.Services.DataServices
{
    public class JokesServices : IJokesService
    {
        private readonly IRepository<Joke> jokeRepository;

        public JokesServices(IRepository<Joke> jokeRepository)
        {
            this.jokeRepository = jokeRepository;
        }

        public IEnumerable<JokeViewModel> GetRandomJokes(int count)
        {
            var jokes = jokeRepository.All()
                .OrderBy(x => Guid.NewGuid())
                .To<JokeViewModel>()
                .Take(count)
                .ToArray();

            return jokes;
        }

        public JokeViewModel GetById(int id)
        {
            Joke joke = jokeRepository.All()
                .Include(j => j.Category)
                .FirstOrDefault(x => x.Id == id);

            JokeViewModel jokeViewModel = joke.To<Joke, JokeViewModel>();
            return jokeViewModel;
        }
    }
}
