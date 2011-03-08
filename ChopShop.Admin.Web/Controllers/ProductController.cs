using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ChopShop.Admin.Services.Interfaces;
using ChopShop.Admin.Web.Models;
using ChopShop.Admin.Web.Models.ViewModel;
using ChopShop.Configuration;
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
        [TransactionFilter(TransactionFilterType.ReadUncommitted)]
        public ActionResult Edit(int id)
        {
            var productEntity = productService.GetSingle(id) ?? new Product();
            var product = new EditProduct();
            product.LoadFromEntity(productEntity);
            
            return View(product);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return Edit(0);
        }

        [HttpPost]
        [TransactionFilter(TransactionFilterType.ReadCommitted)]
        public ActionResult Add(EditProduct product)
        {
            if (!ModelState.IsValid && !product.IsValid())
            {
                AddModelStateErrors(product.Errors());
            }
           

            return View("Edit", product);
        }

        private void AddModelStateErrors(IEnumerable<ErrorInfo> errors)
        {
            foreach (var error in errors)
            {
                ModelState.AddModelError(error.Key, error.Value);
            }
        }
    }
}
