using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChopShop.Model;

namespace ChopShop.Admin.Web.Controllers
{
    public class HomeController : ChopShopController
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to Chop Shop Admin!";
         
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
