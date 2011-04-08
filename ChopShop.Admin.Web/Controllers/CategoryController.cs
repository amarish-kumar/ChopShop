using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ChopShop.Admin.Services.Interfaces;
using ChopShop.Admin.Web.Models;
using ChopShop.Admin.Web.Models.ViewModel;
using ChopShop.Configuration;
using System.Linq;
using ChopShop.Model;

namespace ChopShop.Admin.Web.Controllers
{
    public class CategoryController : ChopShopController
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        [TransactionFilter(TransactionFilterType.ReadUncommitted)]
        public JsonResult _CategoriesForSelectDialog(Guid id)
        {
            var categoriesForProduct = categoryService.ListCategoriesForProduct(id);
            var allCategories = categoryService.List();
            var allCategoriesForDialog = new EditCategory().CategoryList(allCategories, id,
                                                                                    categoriesForProduct);

            return Json(allCategoriesForDialog, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [TransactionFilter(TransactionFilterType.ReadUncommitted)]
        public JsonResult _CategoriesForProduct(Guid id)
        {
            var categoriesForProduct = categoryService.ListCategoriesForProduct(id);

            var categories = new EditCategory().CategoryList(categoriesForProduct, id);
            return Json(categories, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [TransactionFilter(TransactionFilterType.ReadCommitted)]
        public JsonResult _AddToProduct(Guid categoryId, Guid productId)
        {
            categoryService.AddProductToCategory(categoryId, productId);
            return Json(true);
        }

        [HttpPost]
        [TransactionFilter(TransactionFilterType.ReadCommitted)]
        public JsonResult _RemoveFromProduct(Guid categoryId, Guid productId)
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
            ViewBag.Title = Localisation.Admin.PageContent.Add;
            ViewBag.Category = Localisation.Admin.PageContent.Category;
            return View("Edit", category);
        }

        [HttpPost]
        [TransactionFilter(TransactionFilterType.ReadCommitted)]
        public ActionResult Add(EditCategory category)
        {
            var categoryEntity = new Category();
            if (ModelState.IsValid)
            {
                categoryEntity = category.ToEntity();
                if (!categoryService.TryAdd(categoryEntity))
                {
                    AddModelStateErrors(categoryEntity.Errors);
                }
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Title = Localisation.Admin.PageContent.Add;
                ViewBag.Category = Localisation.Admin.PageContent.Category;
                return View("Edit", category);
            }

            return RedirectToAction("Edit", new {id = categoryEntity.Id});
        }

        [HttpGet]
        [TransactionFilter(TransactionFilterType.ReadUncommitted)]
        public ViewResult Edit(Guid id)
        {
            var categoryEntity = categoryService.GetSingle(id) ?? new Category();
            if (categoryEntity.Id == Guid.Empty)
            {
               ModelState.AddModelError("", Localisation.ViewModels.EditCategory.CategoryNotFound);
            }

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
            var categoryEntity = new Category();
            if (ModelState.IsValid)
            {
                categoryEntity = category.ToEntity();
                if (!categoryService.TryAdd(categoryEntity))
                {
                    AddModelStateErrors(categoryEntity.Errors);
                }
            }

            if (!ModelState.IsValid)
            {
                return Json(categoryEntity.Errors);
            }
           
            return Json(categoryEntity.Id);
        }
    }
}
