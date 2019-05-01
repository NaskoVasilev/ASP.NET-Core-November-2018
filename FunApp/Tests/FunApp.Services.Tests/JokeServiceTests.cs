using AutoMapper;
using FunApp.Data;
using FunApp.Data.Common;
using FunApp.Data.Models;
using FunApp.Services.DataServices;
using FunApp.Services.DataServices.Contracts;
using FunApp.Services.Models.Home;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Xunit;

namespace FunApp.Services.Tests
{
    public class JokeServiceTests
    {
        [Fact]
        public void GetByIdShouldReturnsCorrectJokeWithCorrectData()
        {
            var options = new DbContextOptionsBuilder<FunAppDbContext>()
                .UseInMemoryDatabase(databaseName: "JokeById")
                .Options;

            using (var context = new FunAppDbContext(options))
            {
                context.Jokes.AddRange(GetTestData());
                context.SaveChanges();

                IRepository<Joke> repository = new DbRepository<Joke>(context);
                IJokesService jokesService = new JokesServices(repository);
                Mapper.Initialize(configuration =>
                {
                    configuration.CreateMap<Joke, JokeViewModel>();
                });

                JokeViewModel joke = jokesService.GetById(2);
                Assert.Equal("Liverpool will won Premier League in season 2018/2019!!!", joke.Content);
                Assert.Equal(6, joke.Rating);
                Assert.Equal(2, joke.CategoryId);
                Assert.Equal("Football", joke.CategoryName);
            }
        }

        public IList<Joke> GetTestData()
        {
            return new List<Joke>()
            {
                new Joke
                {
                    Id = 1,
                    Content = "Some fun talk",
                    RatingVotes = 5,
                    TotalRating = 24,
                    Category = new Category
                    {
                        Id = 1,
                        Name = "Fun"
                    }
                },
                 new Joke
                {
                     Id = 2,
                    Content = "Liverpool will won Premier League in season 2018/2019!!!",
                    RatingVotes = 10,
                    TotalRating = 60,
                    Category = new Category
                    {
                        Id = 2,
                        Name = "Football"
                    }
                }
            };
        }
    }
}
