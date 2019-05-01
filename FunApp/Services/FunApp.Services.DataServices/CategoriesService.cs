using FunApp.Data.Common;
using FunApp.Data.Models;
using FunApp.Services.DataServices.Contracts;
using FunApp.Services.Mapping;
using FunApp.Services.Models.Categories;
using FunApp.Services.Models.Home;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FunApp.Services.DataServices
{
    public class CategoriesService : ICategoriesService
    {
        private readonly IRepository<Category> categoryRepository;

        public CategoriesService(IRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public CategoryByNameAndIdViewModel[] GetAll()
        {
            var categories = categoryRepository.All()
                .Include(x => x.Jokes)
                .To<CategoryByNameAndIdViewModel>()
                .ToArray();

            return categories;
        }

        public int? GetGategoryId(string category)
        {
            return categoryRepository.All().FirstOrDefault(x => x.Name == category)?.Id;
        }

        public bool IsCategoryIdValid(int id)
        {
            return categoryRepository.All().Any(x => x.Id == id);
        }
    }
}
