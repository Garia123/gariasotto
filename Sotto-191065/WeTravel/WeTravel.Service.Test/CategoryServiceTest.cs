using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using WeTravel.DataAccessInterface;
using WeTravel.Domain;
using WeTravel.Model;

namespace WeTravel.Service.Test
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class CategoryServiceTest
    {
        [TestMethod]
        public void Get()
        {
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var categoriesMock = new List<Category>()
            {
                new Category()
                {
                    Id = id1,
                    Name = "Cat1"
                },
                new Category()
                {
                    Id = id2,
                    Name = "Cat1"
                }
            };            
            var repoMock = new Mock<ICategoryRepository>(MockBehavior.Strict);
            repoMock.Setup(r => r.Get()).Returns(categoriesMock);
            var mockUOW = new Mock<IUnitOfWork>(MockBehavior.Strict);
            mockUOW.SetupGet(u => u.CategoryRepository).Returns(repoMock.Object);
            var service = new CategoryService(mockUOW.Object);

            var result = service.Get();

            repoMock.VerifyAll();
            Assert.IsTrue(((CategoryModelOut)result.ToArray().GetValue(0)).Id == id1);
            Assert.IsTrue(((CategoryModelOut)result.ToArray().GetValue(1)).Id == id2);
        }
    }
}
