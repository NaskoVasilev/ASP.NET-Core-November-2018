using FunApp.Services.Models.Home;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunApp.Services.DataServices.Contracts
{
    public interface IJokesService
    {
        IEnumerable<JokeViewModel> GetRandomJokes(int count);

        JokeViewModel GetById(int id);
    }
}
