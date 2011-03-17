//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Web;
//using ChopShop.Admin.Services.Interfaces;
//using ChopShop.Model;
//using Moq;
//using NUnit.Framework;

//namespace ChopShop.Admin.Services.Tests
//{
//    [TestFixture]
//    public class AdminAuthenticationServiceTests
//    {
//        private Mock<IAdminService> adminService;
//        private Mock<IFormsAuthentication> formsAuthentication;
//        private AdminAuthenticationService service;

//        [SetUp]
//        public void Setup()
//        {
//            adminService = new Mock<IAdminService>();
//            formsAuthentication = new Mock<IFormsAuthentication>();

//            service = new AdminAuthenticationService(adminService.Object, formsAuthentication.Object);
//        }

//        [Test]
//        public void IsValidUser_should_return_true_when_email_and_password_match_user_in_database()
//        {
//            adminService.Setup(x => x.GetUserForLogin(It.IsAny<string>(), It.IsAny<string>())).Returns(new AdminUser()).
//                Verifiable();

//            var result = service.IsValidUser("fakeEmail", "fakePassword");

//            Assert.That(result, Is.True);
//            adminService.Verify(x=>x.GetUserForLogin("fakeEmail", "fakePassword"), Times.AtLeastOnce());
//        }

//        [Test]
//        public void IsValidUser_should_return_false_when_email_and_password_do_not_match_in_database()
//        {
//            adminService.Setup(x => x.GetUserForLogin(It.IsAny<string>(), It.IsAny<string>())).Returns((AdminUser) null).Verifiable();

//            var result = service.IsValidUser("invalidEmail", "invalidPassword");

//            Assert.That(result, Is.False);
//            adminService.Verify(x=>x.GetUserForLogin("invalidEmail", "invalidPassword"), Times.AtLeastOnce());
//        }

//        [Test]
//        [ExpectedException(typeof(ArgumentNullException))]
//        public void SignIn_should_throw_ArgumentNullException_when_username_is_empty()
//        {
//            formsAuthentication.Setup(x => x.SetAuthCookie(It.IsAny<string>(), It.IsAny<bool>())).Verifiable();
//            var fakeHttpContext = new Mock<HttpSessionStateBase>();

//            service.SignIn(string.Empty, fakeHttpContext.Object);
//            formsAuthentication.Verify(x=>x.SetAuthCookie(It.IsAny<string>(), It.IsAny<bool>()), Times.Never());
//        }

//        [Test]
//        public void SignIn_should_add__when_ConditionOccurs()
//        {
            
//        }
//    }
//}
