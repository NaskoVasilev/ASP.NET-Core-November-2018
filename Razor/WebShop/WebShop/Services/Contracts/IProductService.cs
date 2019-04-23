using WebShop.ViewModels.Product;

namespace WebShop.Services.Contracts
{
    public interface IProductService
    {
        void Add(ProductViewModel product);

        void RemoveById(int id);

        ProductViewModel GetById(int id);

        ProductViewModel[] All();

        void UpdateProductById(int id, ProductViewModel newProduct);

        void OrderProduct(int id, string username);
    }
}
