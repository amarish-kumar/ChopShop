using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ChopShop.Admin.Services.Interfaces;
using ChopShop.Admin.Web.Configuration;
using ChopShop.Admin.Web.Models.ViewModel;
using ChopShop.Configuration;
using System.Linq;
using ChopShop.Model;

namespace ChopShop.Admin.Web.Controllers
{
    public class CategoryController : ChopShopController
    {
        private readonly ICategoryService categoryService;

        public IProductService ProductService { get; set; }

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        [TransactionFilter(TransactionFilterType.ReadUncommitted)]
        public JsonResult CategoriesForSelectDialog(Guid id)
        {
            var categoriesForProduct = categoryService.ListCategoriesForProduct(id);
            var allCategories = categoryService.List()
                                               .Select(x => new EditCategory { Id = x.Id, Name = x.Name, Description = x.Description })
                                               .OrderBy(x=>x.Name)
                                               .ToList();

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
        public JsonResult CategoriesForProduct(Guid id)
        {
            var categoriesForProduct = categoryService.ListCategoriesForProduct(id);
            var categories = categoriesForProduct.ToList()
                                                 .Select(x => new EditCategory {Id = x.Id, Description = x.Description, Name = x.Name})
                                                 .OrderBy(x=>x.Name)
                                                 .ToList();
            return Json(categories, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [TransactionFilter(TransactionFilterType.ReadCommitted)]
        public JsonResult AddToProduct(Guid categoryId, Guid productId)
        {
            categoryService.AddProductToCategory(categoryId, productId);
            return Json(true);
        }

        [HttpPost]
        [TransactionFilter(TransactionFilterType.ReadCommitted)]
        public JsonResult RemoveFromProduct(Guid categoryId, Guid productId)
        {
            categoryService.RemoveProductFromCategory(categoryId, productId);
            return Json(true);
        }

        public ActionResult List()
        {
            var categories = categoryService.List();
            return View(categories);
        }

        [HttpGet]
        [TransactionFilter(TransactionFilterType.ReadUncommitted)]
        public ActionResult Add()
        {
            var category = new EditCategory();
            return View("Edit", category);
        }

        [HttpPost]
        [TransactionFilter(TransactionFilterType.ReadCommitted)]
        public ActionResult Add(EditCategory category)
        {
            var categoryEntity = category.ToEntity();
            if (!categoryService.TryAdd(categoryEntity))
            {
                AddModelStateErrors(categoryEntity.Errors);
            }

            ViewBag.Title = !ModelState.IsValid ? Localisation.Admin.PageContent.Add : Localisation.Admin.PageContent.Edit;

            ViewBag.Category = Localisation.Admin.PageContent.Category;
            return View("Edit", category);
        }

        [HttpGet]
        [TransactionFilter(TransactionFilterType.ReadUncommitted)]
        public ActionResult Edit(Guid id)
        {
            var categoryEntity = categoryService.GetSingle(id) ?? new Category();
            var category = new EditCategory();
            category.FromEntity(categoryEntity);
            ViewBag.Title = Localisation.Admin.PageContent.Edit;
            ViewBag.Category = Localisation.Admin.PageContent.Category;
            return View(category);
        }

        [HttpPost]
        [TransactionFilter(TransactionFilterType.ReadCommitted)]
        public JsonResult _Add(EditCategory category)
        {
            var categoryEntity = category.ToEntity();
            if (!categoryService.TryAdd(categoryEntity))
            {
                return Json(Guid.Empty);
            }
            return Json(categoryEntity.Id);
        }
    }
}
