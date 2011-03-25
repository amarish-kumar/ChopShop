using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ChopShop.Admin.Services.Interfaces;
using ChopShop.Admin.Web.Models.ViewModel;
using ChopShop.Configuration;
using ChopShop.Model;
using ChopShop.Model.DTO;
using System.Configuration;
using Telerik.Web.Mvc;

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
        public ViewResult List(int size = 0, int page = 0, string orderBy = null)
        {
            Tuple<IEnumerable<Product>, int> result = GetResult(page, orderBy);
            var productList = new ProductListItem().FromEntityList(result.Item1);
            ViewBag.TotalProducts = result.Item2;
            ViewBag.CurrentPage = page;
            return View(productList);
        }

        [HttpGet]
        [TransactionFilter(TransactionFilterType.ReadUncommitted)]
        [GridAction(EnableCustomBinding=true)]
        public ActionResult _List(int size, int page, string orderBy)
        {
            Tuple<IEnumerable<Product>, int> result = GetResult(page, orderBy);
            var productList = new ProductListItem().FromEntityList(result.Item1);
            return View(new GridModel{Data = productList, Total = result.Item2});
        }

        private Tuple<IEnumerable<Product>, int> GetResult(int page, string orderBy)
        {
            var searchCriteria = new ProductListSearchCriteria
                                     {
                                         CurrentPage = page - 1,
                                         Ascending = GetSortDirection(orderBy),
                                         SortBy = GetSortArgument(orderBy),
                                         ResultsPerPage = Convert.ToInt32(ConfigurationManager.AppSettings["ProductsPerPage"])
                                     };
            return productService.List(searchCriteria);
        }

        private string GetSortArgument(string orderBy)
        {
            if (orderBy == "~")
            {
                return string.Empty;
            }

            return string.IsNullOrEmpty(orderBy) ? string.Empty : orderBy.Split('-')[0];
        }

        private bool GetSortDirection(string orderBy)
        {
            if (orderBy == "~")
            {
                return true;
            }

            return string.IsNullOrEmpty(orderBy) ? true : orderBy.Split('-')[1] == "asc";
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

    }
}
