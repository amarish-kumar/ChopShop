using System.Web.Mvc;
using ChopShop.Admin.Services.Interfaces;
using ChopShop.Admin.Web.Models.ViewModel;
using ChopShop.Configuration;
using ChopShop.Model;


namespace ChopShop.Admin.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAdminAuthenticationService adminAuthenticationService;

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
        [TransactionFilter(TransactionFilterType.ReadCommitted)]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                AdminUser adminUser = null;
                if (adminAuthenticationService.IsValidUser(model.UserName, model.Password, adminUser))
                {
                    adminAuthenticationService.SignIn(adminUser, HttpContext.Session);
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
