using FunApp.Data.Common;
using FunApp.Data.Models;
using FunApp.Services.DataServices;
using FunApp.Services.DataServices.Contracts;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FunApp.Services.Tests
{
    public class CategoryServiceTests
    {
        [Fact]
        public void GetCategoryIdShouldReturnsIdWithCorrectData()
        {
            var categoryRepository = new Mock<IRepository<Category>>();
            categoryRepository.Setup(c => c.All())
                .Returns(GetTestData());

            ICategoriesService categoriesService = new CategoriesService(categoryRepository.Object);
            int? actualId = categoriesService.GetGategoryId("Sport");
            Assert.Equal(3, actualId.Value);
        }

        [Fact]
        public void GetCategoryIdShouldReturnsNullWithIncorrectData()
        {
            var categoryRepository = new Mock<IRepository<Category>>();
            categoryRepository.Setup(c => c.All())
                .Returns(GetTestData());

            ICategoriesService categoriesService = new CategoriesService(categoryRepository.Object);
            int? actualId = categoriesService.GetGategoryId("Other");
            Assert.Null(actualId);
        }

        public IQueryable<Category> GetTestData()
        {
            return new List<Category>()
                {
                    new Category { Id = 1, Name = "Animals" },
                    new Category { Id = 2, Name = "Football" },
                    new Category { Id = 3, Name = "Sport" },
                    new Category { Id = 4, Name = "Programmers" },
                }
            .AsQueryable();
        }
    }
}
