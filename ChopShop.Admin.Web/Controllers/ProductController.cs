using System;
using System.Web.Mvc;
using ChopShop.Admin.Services.Interfaces;
using ChopShop.Admin.Web.Configuration;
using ChopShop.Admin.Web.Models.ViewModel;
using ChopShop.Configuration;
using ChopShop.Model;
using ChopShop.Model.DTO;

namespace ChopShop.Admin.Web.Controllers
{
    public class ProductController : ChopShopController
    {
        private readonly IProductService productService;
        public ICategoryService CategoryService { get; set; }

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        [TransactionFilter(TransactionFilterType.ReadUncommitted)]
        public ActionResult List(ProductListSearchCriteria searchCriteria)
        {
            var productEntityList = productService.List(searchCriteria);
            var productList = new ProductListItem().FromEntityList(productEntityList);
            return View(productList);
        }

        [HttpGet]
        [TransactionFilter(TransactionFilterType.ReadUncommitted)]
        public ActionResult Edit(Guid id)
        {
            var productEntity = productService.GetSingle(id) ?? new Product();
            var product = new EditProduct();
            product.FromEntity(productEntity);
            ViewBag.Title = Localisation.Admin.PageContent.Edit;
            ViewBag.Product = Localisation.Admin.PageContent.Product;
            ViewBag.ViewType = "Edit";
            return View(product);
        }

        [HttpGet]
        [TransactionFilter(TransactionFilterType.ReadUncommitted)]
        public ActionResult Add()
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
            return RedirectToAction("Edit", new {id = productEntity.Id});
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
       
    }
}
