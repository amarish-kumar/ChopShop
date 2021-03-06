﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using ChopShop.Admin.Services.Interfaces;
using ChopShop.Admin.Web.Models.DTO;
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
        public ViewResult List(int page = 0, int perPage = 25, string orderBy = null, string asc = "true")
        {
            Tuple<IEnumerable<Product>, int> result = GetProductList(page, orderBy, asc, perPage);
            var productList = new ProductListItem().FromEntityList(result.Item1);
            var pagingDto = new PagingDTO {TotalItems = result.Item2, CurrentPage = page, PerPage = perPage, TotalPages = GetTotalPages(result.Item2, perPage)};
            ViewBag.Paging = pagingDto;
            return View(productList);
        }

        private int GetTotalPages(int totalItems, int perPage)
        {
            if (totalItems % perPage == 0)
            {
                return totalItems/perPage;
            }

            return Convert.ToInt32(Math.Floor((double) (totalItems/perPage)) + 1);
        }

        //[HttpGet]
        //[TransactionFilter(TransactionFilterType.ReadUncommitted)]
        //public ActionResult _List(int page, int perPage, string orderBy, bool ascending)
        //{
        //    Tuple<IEnumerable<Product>, int> result = GetProductList(page, orderBy, ascending, perPage);
        //    var productList = new ProductListItem().FromEntityList(result.Item1);
        //    return View(new GridModel{Data = productList, Total = result.Item2});
        //}

        private Tuple<IEnumerable<Product>, int> GetProductList(int page, string orderBy, string ascending, int perPage)
        {
            var searchCriteria = new ProductListSearchCriteria
                                     {
                                         CurrentPage = page - 1,
                                         Ascending = ascending.ToUpper() == "TRUE",
                                         SortBy = orderBy,
                                         ResultsPerPage = perPage
                                     };
            return productService.List(searchCriteria);
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
        public JsonResult _AddPrice(EditPrice price)
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
        public ViewResult DeleteConfirmation(Guid id, int page = 0, int perPage = 25, string orderBy = null, string asc = "true")
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
        public RedirectToRouteResult Delete(Guid id,int page = 0, int perPage = 25, string orderBy = null, string asc = "true")
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
            return RedirectToAction("List", new {page = page, perPage = perPage, orderBy = orderBy, asc = asc});
        }
    }
}
