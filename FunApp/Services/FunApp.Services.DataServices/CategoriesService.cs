using FunApp.Data.Common;
using FunApp.Data.Models;
using FunApp.Services.DataServices.Contracts;
using FunApp.Services.Mapping;
using FunApp.Services.Models.Categories;
using FunApp.Services.Models.Home;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FunApp.Services.DataServices
{
    public class CategoriesService : ICategoriesService
    {
        private readonly IRepository<Category> categoryRepository;

        public CategoriesService(IRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task Create(string name)
        {
            await this.categoryRepository.AddAsync(new Category() { Name = name });
            await categoryRepository.SaveChangesAsync();
        }

        public async Task Edit(string name, int id)
        {
            Category category = this.GetById(id);
            if(category != null)
            {
                category.Name = name;
                await categoryRepository.SaveChangesAsync();
            }
        }

        public CategoryByNameAndIdViewModel[] GetAll()
        {
            var categories = categoryRepository.All()
                .Include(x => x.Jokes)
                .To<CategoryByNameAndIdViewModel>()
                .ToArray();

            return categories;
        }

        public Category GetById(int id)
        {
            return this.categoryRepository.All().FirstOrDefault(x => x.Id == id);
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
