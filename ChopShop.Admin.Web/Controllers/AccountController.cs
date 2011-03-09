using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using ChopShop.Admin.Services.Interfaces;
using ChopShop.Admin.Web.Models;
using ChopShop.Configuration;

namespace ChopShop.Admin.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAdminAuthenticationService adminAuthenticationService;
        public IFormsAuthenticationService FormsService { get; set; }
        public IMembershipService MembershipService { get; set; }

        public AccountController(IAdminAuthenticationService adminAuthenticationService)
        {
            this.adminAuthenticationService = adminAuthenticationService;
        }

        public ViewResult LogOn()
        {
            adminAuthenticationService.SignOut(HttpContext.Session);
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (adminAuthenticationService.IsValidUser(model.UserName, model.Password))
                {
                    adminAuthenticationService.SignIn(model.UserName, HttpContext.Session);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    
                    return RedirectToAction("Index", "Home");
                }
                
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // **************************************
        // URL: /Account/LogOff
        // **************************************

        public ActionResult LogOff()
        {
            adminAuthenticationService.SignOut(HttpContext.Session);

            return RedirectToAction("Index", "Home");
        }
   
    }
}
