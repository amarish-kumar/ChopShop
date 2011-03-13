using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ChopShop.Admin.Services.Interfaces;
using ChopShop.Admin.Web.Models.ViewModel;
using ChopShop.Configuration;
using System.Linq;

namespace ChopShop.Admin.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;

        public IProductService ProductService { get; set; }

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        [TransactionFilter(TransactionFilterType.ReadUncommitted)]
        public JsonResult CategoriesForSelectDialog(int id)
        {
            var categoriesForProduct = categoryService.ListCategoriesForProduct(id);
            var allCategories = categoryService.List().Select(x => new EditCategory { Id = x.Id, Name = x.Name, Description = x.Description }).ToList();

            allCategories.ForEach(x=>
                                      {
                                          if (categoriesForProduct.FirstOrDefault(y => y.Id == x.Id) != null)
                                          {
                                              x.IsInProduct = true;
                                          }
                                      });
            
            return Json(allCategories, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [TransactionFilter(TransactionFilterType.ReadUncommitted)]
        public JsonResult CategoriesForProduct(int id)
        {
            var categoriesForProduct = categoryService.ListCategoriesForProduct(id);
            return Json(categoriesForProduct, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [TransactionFilter(TransactionFilterType.ReadCommitted)]
        public JsonResult AddToProduct(int categoryId, int productId)
        {
            //var product = ProductService.GetSingle(productId);
            categoryService.AddProductToCategory(categoryId, productId);
            return Json(true);
        }

        [HttpPost]
        [TransactionFilter(TransactionFilterType.ReadCommitted)]
        public JsonResult RemoveFromProduct(int categoryId, int productId)
        {
            categoryService.RemoveProductFromCategory(categoryId, productId);
            return Json(true);
        }

        public ActionResult List()
        {
            var categories = categoryService.List();
            return View(categories);
        }
    }
}
