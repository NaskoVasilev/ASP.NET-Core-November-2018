using FunApp.Services.Models.Home;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FunApp.Services.DataServices.Contracts
{
    public interface IJokesService
    {
        IEnumerable<JokeViewModel> GetRandomJokes(int count);

        JokeViewModel GetById(int id);

        Task<int> Create(string content, int categoryId);

        string NormalizeJoke(string joke);

        IEnumerable<JokeViewModel> JokesByCategory(int categoryId);

        IEnumerable<JokeViewModel> GetAll();

        Task RateJoke(int rating, int jokeId);

        double GetRate(int jokeId);
    }
}
