using FunApp.Data.Models;
using FunApp.Services.Models.Categories;
using System.Threading.Tasks;

namespace FunApp.Services.DataServices.Contracts
{
    public interface ICategoriesService
    {
        CategoryByNameAndIdViewModel[] GetAll();

        bool IsCategoryIdValid(int id);

        int? GetGategoryId(string category);

        Task Create(string name);

        Task Edit(string name, int id);

        Category GetById(int id);
    }
}
