using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.CodeAnalysis;
using WeTravel.Domain.Exceptions;

namespace WeTravel.Domain.Test
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class RegionTest
    {
        [TestMethod]
        public void RegionValid()
        {
            var region = new Region()
            {
                Id = Guid.NewGuid(),
                Name = "Region valid"
            };

            region.ValidateEntity();
        }
    }
}
