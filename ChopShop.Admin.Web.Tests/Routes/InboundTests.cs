using System.Web;
using System.Web.Routing;
using Moq;
using NUnit.Framework;

namespace ChopShop.Admin.Web.Tests.Routes
{
    [TestFixture]
    public class InboundTests
    {
        [Test]
        public void Root_of_Product_should_return_Product_List()
        {
            TestRoute("~/Product", new{controller="Product", action="List"});
        }

        private static void TestRoute(string url, object expectedControllerAction)
        {
            var routes = new RouteCollection();
            var routeMapper = new Configuration.RouteMapper(routes);
            routeMapper.RegisterRoutes();
            var mockHttpContext = new Mock<HttpContextBase>();
            var mockRequest = new Mock<HttpRequestBase>();
            mockHttpContext.Setup(x => x.Request).Returns(mockRequest.Object);
            mockRequest.Setup(x => x.AppRelativeCurrentExecutionFilePath).Returns(url);

            RouteData routeData = routes.GetRouteData(mockHttpContext.Object);

            Assert.IsNotNull(routeData);
            var expectedRoutes = new RouteValueDictionary(expectedControllerAction);
            foreach (var route in expectedRoutes)
            {
                if (route.Value == null)
                {
                    Assert.IsNull(routeData.Values[route.Key]);
                }
                else
                {
                    Assert.AreEqual(route.Value.ToString(), routeData.Values[route.Key].ToString());
                }
            }
        }
    }
}
