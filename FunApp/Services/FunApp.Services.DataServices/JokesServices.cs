using FunApp.Data.Common;
using FunApp.Data.Models;
using FunApp.Services.DataServices.Contracts;
using FunApp.Services.Mapping;
using FunApp.Services.Models.Home;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<int> Create(string content, int categoryId)
        {
            Joke joke = new Joke
            {
                Content = content,
                CategoryId = categoryId
            };

            await jokeRepository.AddAsync(joke);
            await jokeRepository.SaveChangesAsync();
            return joke.Id;
        }

        public string NormalizeJoke(string joke)
        {
            string[] separators = new string[] { ",", ".", "!", "\'", " ", "\'s", "?", ":", "\n", "-" };

            string[] words = joke.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            string normalizedJoke = string.Join(" ", words);
            return normalizedJoke;
        }

        public IEnumerable<JokeViewModel> JokesByCategory(int categoryId)
        {
            return jokeRepository.All()
                .Where(j => j.CategoryId == categoryId)
                .To<JokeViewModel>()
                .ToArray();
        }

        public IEnumerable<JokeViewModel> GetAll()
        {
            return this.jokeRepository.All()
                .To<JokeViewModel>()
                .ToArray();
        }

        public async Task RateJoke(int rating, int jokeId)
        {
            Joke joke = jokeRepository.All().FirstOrDefault(x => x.Id == jokeId);
            if (joke != null && rating > 0 && rating <= 6)
            {
                joke.TotalRating += rating;
                joke.RatingVotes++;
                await jokeRepository.SaveChangesAsync();
            }
        }

        public double GetRate(int jokeId)
        {
            return this.jokeRepository.All().FirstOrDefault(x => x.Id == jokeId)?.Rating ?? 0;
        }
    }
}
