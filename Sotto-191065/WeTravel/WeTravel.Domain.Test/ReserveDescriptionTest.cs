using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.CodeAnalysis;
using WeTravel.Domain.Exceptions;

namespace WeTravel.Domain.Test
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ReserveDescriptionTest
    {
        [TestMethod]
        public void Valid()
        {
            ReserveDescription reserveDescription = CreateReserveDescription();

            reserveDescription.ValidateEntity();
        }

        [TestMethod]
        [ExpectedException(typeof(FormatExceptionBeautifier))]
        public void MissingReserveId()
        {
            ReserveDescription reserveDescription = CreateReserveDescription();
            reserveDescription.ReserveId = new Guid();

            reserveDescription.ValidateEntity();
        }
        
        private ReserveDescription CreateReserveDescription()
        {
            ReserveDescription reserveDescription = new ReserveDescription()
            {
                ReserveId = Guid.NewGuid(),
                Description = "hola",
                State = ReserveState.CREATED
            };

            return reserveDescription;
        }
    }
}
