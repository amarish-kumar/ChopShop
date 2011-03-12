using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ChopShop.Admin.Services.Interfaces;
using ChopShop.Admin.Web.Models.ViewModel;
using ChopShop.Configuration;

namespace ChopShop.Admin.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        [TransactionFilter(TransactionFilterType.ReadUncommitted)]
        public JsonResult List()
        {
            var categoryEntities = categoryService.List();
            var category = new List<EditCategory>();
            foreach (var entity in categoryEntities)
            {
                var newCategory = new EditCategory();
                newCategory.FromEntity(entity);
                category.Add(newCategory);
            }
            return Json(category, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CategoriesForProduct(int productId)
        {
            throw new NotImplementedException();
        }
    }
}
