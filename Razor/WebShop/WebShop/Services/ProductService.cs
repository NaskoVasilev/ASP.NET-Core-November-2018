using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Linq;
using WebShop.Data;
using WebShop.Models;
using WebShop.Services.Contracts;
using WebShop.ViewModels.Product;

namespace WebShop.Services
{
    public class ProductService : IProductService
    {
        private readonly WebShopDbContext context;
        private readonly IMapper mapper;

        public ProductService(WebShopDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public void Add(ProductViewModel product)
        {
            Product validProduct = mapper.Map<Product>(product);
            context.Products.Add(validProduct);
            context.SaveChanges();
        }

        public ProductViewModel[] All()
        {
            return context.Products
                .ProjectTo<ProductViewModel>(mapper.ConfigurationProvider)
                .ToArray();
        }

        public ProductViewModel GetById(int id)
        {
            Product product = GetProductById(id);
            return mapper.Map<ProductViewModel>(product);
        }

        public void OrderProduct(int id, string username)
        {
            User user = context.Users.FirstOrDefault(u => u.UserName == username);
            Order order = new Order()
            {
                ClientId = user.Id,
                ProductId = id
            };
            context.Orders.Add(order);
            context.SaveChanges();
        }

        public void RemoveById(int id)
        {
            Product product = GetProductById(id);
            context.Products.Remove(product);
            context.SaveChanges();
        }

        public void UpdateProductById(int id, ProductViewModel newProduct)
        {
            Product product = GetProductById(id);
            product.Name = newProduct.Name;
            product.Price = newProduct.Price;
            product.Type = newProduct.Type;
            product.Description = newProduct.Description;
            context.Products.Update(product);
            context.SaveChanges();
        }

        private Product GetProductById(int id)
        {
            return context.Products.Find(id); 
        }
    }
}
