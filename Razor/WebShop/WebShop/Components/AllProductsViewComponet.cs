using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Services.Contracts;
using WebShop.ViewModels.Product;

namespace WebShop.Components
{
    [ViewComponent(Name = "AllProducts")]
    public class AllProductsViewComponet : ViewComponent
    {
        private readonly IProductService service;

        public AllProductsViewComponet(IProductService service)
        {
            this.service = service;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ProductViewModel[] products = service.All();
            return View(products);
        }
    }
}
