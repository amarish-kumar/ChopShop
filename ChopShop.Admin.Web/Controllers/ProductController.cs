using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using ChopShop.Admin.Services.Interfaces;
using ChopShop.Admin.Web.Models.ViewModel;
using ChopShop.Model;

namespace ChopShop.Admin.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public ActionResult List()
        {
            return View(productService.List());
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var productEntity = productService.GetSingle(id) ?? new Product();
            var product = new EditProduct();
            Mapper.CreateMap<Product, EditProduct>();
            //Mapper.CreateMap<Cost, EditCost>();
            Mapper.Map(productEntity, product);
            
            return View(product);
        }

    }
}
