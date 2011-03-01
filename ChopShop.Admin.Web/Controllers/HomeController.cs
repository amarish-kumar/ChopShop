using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChopShop.Admin.Web.Controllers
{
    public class HomeController : Controller
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
