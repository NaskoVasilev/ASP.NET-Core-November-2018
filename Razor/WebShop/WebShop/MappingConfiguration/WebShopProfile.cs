using AutoMapper;
using WebShop.Models;
using WebShop.ViewModels.Product;

namespace WebShop.MappingConfiguration
{
    public class WebShopProfile : Profile
    {
        public WebShopProfile()
        {
            this.CreateMap<ProductViewModel, Product>()
                .ReverseMap();
        }
    }
}
