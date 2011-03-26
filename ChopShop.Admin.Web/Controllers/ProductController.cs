using System;
using System.Web.Mvc;
using ChopShop.Admin.Services.Interfaces;
using ChopShop.Admin.Web.Models.ViewModel;
using ChopShop.Configuration;
using ChopShop.Model;
using ChopShop.Model.DTO;

namespace ChopShop.Admin.Web.Controllers
{
    public class ProductController : ChopShopController
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        [TransactionFilter(TransactionFilterType.ReadUncommitted)]
        public ViewResult List(ProductListSearchCriteria searchCriteria)
        {
            var productEntityList = productService.List(searchCriteria);
            var productList = new ProductListItem().FromEntityList(productEntityList);
            return View(productList);
        }

        [HttpGet]
        [TransactionFilter(TransactionFilterType.ReadUncommitted)]
        public ViewResult Edit(Guid id)
        {
            var productEntity = productService.GetSingle(id) ?? new Product();

            if (productEntity.Id == Guid.Empty)
            {
                ModelState.AddModelError("", Localisation.ViewModels.EditProduct.ProductNotFound);
            }

            var product = new EditProduct();
            product.FromEntity(productEntity);
            ViewBag.Title = Localisation.Admin.PageContent.Edit;
            ViewBag.Product = Localisation.Admin.PageContent.Product;
            ViewBag.ViewType = "Edit";
            return View(product);
        }

        [HttpGet]
        [TransactionFilter(TransactionFilterType.ReadUncommitted)]
        public ViewResult Add()
        {
            var product = new EditProduct();
            ViewBag.Title = Localisation.Admin.PageContent.Add;
            ViewBag.Product = Localisation.Admin.PageContent.Product;
            ViewBag.ViewType = "Add";
            return View("Edit", product);
        }

        [HttpPost]
        [TransactionFilter(TransactionFilterType.ReadCommitted)]
        public ActionResult Add(EditProduct product)
        {
            var productEntity = new Product();
            if (ModelState.IsValid) // validate inputs first
            {
                productEntity = product.ToEntity();
                if (!productService.TryAdd(productEntity)) // validate business logic
                {
                    AddModelStateErrors(productEntity.Errors);
                }
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Title = Localisation.Admin.PageContent.Add;
                ViewBag.Product = Localisation.Admin.PageContent.Product;
                ViewBag.ViewType = "Add";
                return View("Edit", product);
            }
            return RedirectToAction("Edit", new { id = productEntity.Id });
        }

        [HttpPost]
        [TransactionFilter(TransactionFilterType.ReadCommitted)]
        public JsonResult AddPrice(EditPrice price)
        {
            var priceEntity = price.ToEntity();
            if (!productService.TryAddPrice(priceEntity))
            {
                return Json(false);
            }
            return Json(true);
        }

        [HttpGet]
        [TransactionFilter(TransactionFilterType.ReadUncommitted)]
        public ViewResult Delete(Guid id)
        {
            var productEntity = productService.GetSingle(id) ?? new Product();

            if (productEntity.Id == Guid.Empty)
            {
                ModelState.AddModelError("", Localisation.ViewModels.EditProduct.ProductNotFound);
            }

            var product = new EditProduct();
            product.FromEntity(productEntity);
            ViewBag.Title = Localisation.Admin.PageContent.Delete;
            ViewBag.Product = Localisation.Admin.PageContent.Product;
            ViewBag.ViewType = "Delete";
            return View(product);
        }

        [HttpPost]
        [TransactionFilter(TransactionFilterType.ReadCommitted)]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            var productEntity = productService.GetSingle(id) ?? new Product();

            if (productEntity.Id == Guid.Empty)
            {
                ModelState.AddModelError("", Localisation.ViewModels.EditProduct.ProductNotFound);
            }

            if (!productService.TryDelete(id)) // validate business logic
            {
                AddModelStateErrors(productEntity.Errors);
            }
            return RedirectToAction("List");
        }
    }
}
