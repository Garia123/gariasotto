using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using WeTravel.ServiceInterface;
using WeTravel.Filter;

namespace WeTravel.Filters.Test
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class AuthFilterTest
    {
        [TestMethod]
        public void ItIsAuth()
        {
            var serviceMock = new Mock<ISessionService>(MockBehavior.Strict);
            serviceMock.Setup(s => s.ValidateToken(It.IsAny<Guid>())).Returns(true);
            var headers = new HeaderDictionary { { "auth", Guid.NewGuid().ToString() } };
            var mockHttpRequest = new Mock<HttpRequest>();
            mockHttpRequest.Setup(r => r.Headers).Returns(headers);
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(h => h.Request).Returns(mockHttpRequest.Object);
            var context = new ActionContext(mockHttpContext.Object,
                new RouteData(),
                new ActionDescriptor());
            var fakeAuthFilterContext = new AuthorizationFilterContext(context, new List<IFilterMetadata>());
            var filter = new WeTravelAuthFilter(serviceMock.Object);

            filter.OnAuthorization(fakeAuthFilterContext);

            mockHttpRequest.VerifyAll();
            mockHttpContext.VerifyAll();
            serviceMock.VerifyAll();
        }

        [TestMethod]
        public void ItIsNotAuthorized()
        {
            var serviceMock = new Mock<ISessionService>(MockBehavior.Strict);
            serviceMock.Setup(s => s.ValidateToken(It.IsAny<Guid>())).Returns(false);
            var headers = new HeaderDictionary { { "auth", Guid.NewGuid().ToString() } };
            var mockHttpRequest = new Mock<HttpRequest>();
            mockHttpRequest.Setup(r => r.Headers).Returns(headers);
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(h => h.Request).Returns(mockHttpRequest.Object);
            var context = new ActionContext(mockHttpContext.Object,
                new RouteData(),
                new ActionDescriptor());
            var fakeAuthFilterContext = new AuthorizationFilterContext(context, new List<IFilterMetadata>());
            var filter = new WeTravelAuthFilter(serviceMock.Object);

            filter.OnAuthorization(fakeAuthFilterContext);

            mockHttpRequest.VerifyAll();
            mockHttpContext.VerifyAll();
            serviceMock.VerifyAll();
            var result = fakeAuthFilterContext.Result as ContentResult;
            Assert.IsTrue(result.StatusCode == 401);
        }

        [TestMethod]
        public void NonToken()
        {
            var serviceMock = new Mock<ISessionService>(MockBehavior.Strict);
            var headers = new HeaderDictionary { { "auth", "" } };
            var mockHttpRequest = new Mock<HttpRequest>();
            mockHttpRequest.Setup(r => r.Headers).Returns(headers);
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(h => h.Request).Returns(mockHttpRequest.Object);
            var context = new ActionContext(mockHttpContext.Object,
                new RouteData(),
                new ActionDescriptor());
            var fakeAuthFilterContext = new AuthorizationFilterContext(context, new List<IFilterMetadata>());
            var filter = new WeTravelAuthFilter(serviceMock.Object);

            filter.OnAuthorization(fakeAuthFilterContext);

            mockHttpRequest.VerifyAll();
            mockHttpContext.VerifyAll();
            serviceMock.VerifyAll();
            var result = fakeAuthFilterContext.Result as ContentResult;
            Assert.IsTrue(result.StatusCode == 401);
        }
    }
}
