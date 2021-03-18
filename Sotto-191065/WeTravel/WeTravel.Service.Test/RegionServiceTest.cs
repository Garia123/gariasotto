using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeTravel.Model;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeTravel.DataAccessInterface;
using WeTravel.Domain;
using System.Diagnostics.CodeAnalysis;

namespace WeTravel.Service.Test
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class RegionServiceTest
    {
        [TestMethod]
        public void Get()
        {
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var regionMock = new List<Region>()
            {
                new Region()
                {
                    Id = id1,
                    Name = "Region 1"
                },
                new Region()
                {
                    Id = id2,
                    Name = "Region 2"
                }
            };
            var regionResult = new List<RegionModelOut>()
            {
                new RegionModelOut()
                {
                    Id = ((Region)regionMock.ToArray().GetValue(0)).Id,
                    Name = ((Region)regionMock.ToArray().GetValue(0)).Name,
                },
                new RegionModelOut()
                {
                    Id = ((Region)regionMock.ToArray().GetValue(1)).Id,
                    Name = ((Region)regionMock.ToArray().GetValue(1)).Name,
                }
            };
            var repoMock = new Mock<IRegionRepository>(MockBehavior.Strict);
            repoMock.Setup(r => r.Get()).Returns(regionMock);
            var mockUOW = new Mock<IUnitOfWork>(MockBehavior.Strict);
            mockUOW.SetupGet(u => u.RegionRepository).Returns(repoMock.Object);
            var service = new RegionService(mockUOW.Object);

            var result = service.Get();

            repoMock.VerifyAll();
            mockUOW.VerifyAll();
            Assert.IsTrue(((RegionModelOut)result.ToArray().GetValue(0)).Id == id1);
            Assert.IsTrue(((RegionModelOut)result.ToArray().GetValue(1)).Id == id2);
        }
    }
}
