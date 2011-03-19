using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ChopShop.Admin.Services.Interfaces;
using ChopShop.Admin.Web.Controllers;
using ChopShop.Admin.Web.Models.ViewModel;
using ChopShop.Model;
using Moq;
using NUnit.Framework;

namespace ChopShop.Admin.Web.Tests.Controllers
{
    [TestFixture]
    public class AccountControllerTests
    {
        private Mock<IAdminAuthenticationService> service;
        private AccountController controller;

        [SetUp]
        public void Setup()
        {
            service = new Mock<IAdminAuthenticationService>();
            controller = new AccountController(service.Object);
        }

        [Test]
        public void LogOn_should_log_user_out_first_when_invoked()
        {
            service.Setup(x => x.SignOut(It.IsAny<HttpSessionStateBase>())).Verifiable();
            controller.ControllerContext = Fakes.MockedContext().Object;

            var action = controller.LogOn();

            Assert.That(action, Is.Not.Null);
            service.Verify(x=>x.SignOut(It.IsAny<HttpSessionStateBase>()), Times.AtLeastOnce());
        }

        [Test]
        public void LogOn_should_not_sign_user_in_when_user_details_are_invalid()
        {
            AdminUser adminUser = null;
            service.Setup(x => x.IsValidUser(It.IsAny<string>(), It.IsAny<string>(), out adminUser)).Returns(
                false).Verifiable();
            service.Setup(x => x.SignIn(It.IsAny<AdminUser>(), It.IsAny<HttpSessionStateBase>())).Verifiable();

            var action = controller.LogOn(new LogOnModel(), string.Empty);

            Assert.That(action, Is.Not.Null);
            Assert.That(controller.ModelState.IsValid, Is.False);
            service.Verify(x=>x.IsValidUser(It.IsAny<string>(), It.IsAny<string>(), out adminUser), Times.AtLeastOnce());
            service.Verify(x=>x.SignIn(It.IsAny<AdminUser>(), It.IsAny<HttpSessionStateBase>()), Times.Never());
        }

        [Test]
        public void LogOn_should_redirect_to_URL_requested_when_user_is_valid_and_url_is_local()
        {
            AdminUser adminUser = new AdminUser();
            service.Setup(x => x.IsValidUser(It.IsAny<string>(), It.IsAny<string>(), out adminUser)).Returns(true).Verifiable();
            service.Setup(x => x.SignIn(It.IsAny<AdminUser>(), It.IsAny<HttpSessionStateBase>())).Verifiable();
            var routes = new RouteCollection();
            controller.ControllerContext = Fakes.MockedContext().Object;
            controller.Url = new UrlHelper(new RequestContext(Fakes.MockedContext().Object.HttpContext, new RouteData()), routes);

            var action = controller.LogOn(new LogOnModel(), "/Product/List") as RedirectResult;

            Assert.That(action.Url, Is.EqualTo("/Product/List"));
        }

        [Test]
        [Ignore("Cannot fake/mock IsUrlLocalToHost static method")]
        public void LogOn_should_redirect_to_Index_action_on_HomeController_when_user_is_valid_and_url_is_not_local()
        {
            AdminUser adminUser = new AdminUser();
            service.Setup(x => x.IsValidUser(It.IsAny<string>(), It.IsAny<string>(), out adminUser)).Returns(true).Verifiable();
            service.Setup(x => x.SignIn(It.IsAny<AdminUser>(), It.IsAny<HttpSessionStateBase>())).Verifiable();
            //var mockUrlHelper = new Mock<UrlHelper>();
            //mockUrlHelper.Setup(x => x.IsLocalUrl(It.IsAny<string>())).Returns(false);
            var controllerContext = Fakes.MockedContext();
            var routes = new RouteCollection();


            controller.ControllerContext = controllerContext.Object;
            controller.Url = new UrlHelper(new RequestContext(controllerContext.Object.HttpContext, new RouteData()), routes);

            var action = controller.LogOn(new LogOnModel(), "http://www.disney.com") as RedirectResult;

            Assert.That(action.Url, Is.EqualTo("/Home/Index"));
        }

        [Test]
        public void LogOff_should_call_SignOut_on_Service_and_redirect_when_invoked()
        {
            service.Setup(x => x.SignOut(It.IsAny<HttpSessionStateBase>())).Verifiable();
            controller.ControllerContext = Fakes.MockedContext().Object;

            var action = controller.LogOff() as RedirectToRouteResult;

            Assert.That(action, Is.Not.Null);
            Assert.That(action.RouteValues["Action"], Is.EqualTo("LogOn"));
            Assert.That(action.RouteValues["Controller"], Is.EqualTo("Account"));
            service.Verify(x=>x.SignOut(It.IsAny<HttpSessionStateBase>()), Times.AtLeastOnce());
        }
    }
}
