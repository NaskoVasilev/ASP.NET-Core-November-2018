using FunApp.Services.Models.Categories;

namespace FunApp.Services.DataServices.Contracts
{
    public interface ICategoriesService
    {
        CategoryByNameAndIdViewModel[] GetAll();

        bool IsCategoryIdValid(int id);

        int? GetGategoryId(string category);
    }
}
