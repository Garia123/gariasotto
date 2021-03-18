using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using WeTravel.ServiceInterface;
using WeTravel.Domain;
using WeTravel.Model;

namespace WeTravel.WebApi.Test
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class CategoryControllerTest
    {
        [TestMethod]
        public void GetCategories()
        {
            var categories = new List<CategoryModelOut>()
            {
                new CategoryModelOut()
                {
                    Id = Guid.NewGuid(),
                    Name = "Hotels",
                },
                new CategoryModelOut()
                {
                    Id = Guid.NewGuid(),
                    Name = "Spas",
                },

            };
            var mock = new Mock<ICategoryService>(MockBehavior.Strict);
            mock.Setup(m => m.Get()).Returns(categories);
            var controller = new CategoryController(mock.Object);

            var result = controller.Get();
            var categoriesResult = result as OkObjectResult;
            var resultList = categoriesResult.Value as IEnumerable<CategoryModelOut>;

            mock.VerifyAll();
            Assert.IsTrue(categories.SequenceEqual(resultList));
        }
    }
}
