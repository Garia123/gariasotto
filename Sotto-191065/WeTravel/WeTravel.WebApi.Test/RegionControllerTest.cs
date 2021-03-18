using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeTravel.Model;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using WeTravel.ServiceInterface;
using System.Diagnostics.CodeAnalysis;

namespace WeTravel.WebApi.Test
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class RegionControllerTest
    {
        [TestMethod]
        public void GetRegions()
        {
            var regions = new List<RegionModelOut>()
            {
                new RegionModelOut()
                {
                    Id = Guid.NewGuid(),
                    Name = "Punta del Este",
                },
                new RegionModelOut()
                {
                    Id = Guid.NewGuid(),
                    Name = "Montevideo",
                },
            };
            var mock = new Mock<IRegionService>(MockBehavior.Strict);
            mock.Setup(m => m.Get()).Returns(regions);
            var controller = new RegionController(mock.Object);

            var result = controller.Get();
            var categoriesResult = result as OkObjectResult;
            var resultList = categoriesResult.Value as IEnumerable<RegionModelOut>;

            mock.VerifyAll();
            Assert.IsTrue(regions.SequenceEqual(resultList));
        }
    }
}
