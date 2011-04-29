using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChopShop.Admin.Services.Interfaces;

namespace ChopShop.Shop.Web.Controllers
{
    public class HomeController : Controller
    {
        private ICategoryService categoryService;

        public HomeController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NavigationLinks()
        {
            var categories = categoryService.List().Take(5);
            throw new NotImplementedException();
        }
    }
}
