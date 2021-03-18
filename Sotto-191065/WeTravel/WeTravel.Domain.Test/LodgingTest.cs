using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using WeTravel.Domain.Exceptions;

namespace WeTravel.Domain.Test
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class LodgingTest
    {
        [TestMethod]
        public void Valid()
        {
            var entity = new Lodging()
            {
                Id = Guid.NewGuid(),
                Name = "Test Name",
                Description = "Test Description",
                Address = "Test address",
                TouristLocation = CreateTouristLocation(),
                Stars = 3,
                PricePerNight = 200,
                Available = true,
                Telephone = "099999799",
                InformationText = "The following text will be displayed when a reservation is made."
            };

            entity.ValidateEntity();
        }

        [TestMethod]
        [ExpectedException(typeof(FormatExceptionBeautifier))]
        public void MissingId()
        {
            var entity = new Lodging()
            {
                Name = "Test Name",
                Description = "Test Description",
                Address = "Test address",
                TouristLocation = CreateTouristLocation(),
                Stars = 3,
                PricePerNight = 200,
                Available = true,
                Telephone = "0999979",
                InformationText = "Test information text"
            };

            entity.ValidateEntity();
        }

        
        [TestMethod]
        [ExpectedException(typeof(FormatExceptionBeautifier))]
        public void StarMoreThan5()
        {
            var entity = new Lodging()
            {
                Id = Guid.NewGuid(),
                Name = "Test Name",
                Description = "Test Description",
                Address = "Test address",
                TouristLocation = CreateTouristLocation(),
                Stars = 10,
                PricePerNight = 200,
                Available = true,
                Telephone = "0999979",
                InformationText = "Test information text"
            };

            entity.ValidateEntity();
        }

        [TestMethod]
        [ExpectedException(typeof(FormatExceptionBeautifier))]
        public void StarLessThan1()
        {
            var entity = new Lodging()
            {
                Id = Guid.NewGuid(),
                Name = "Test Name",
                Description = "Test Description",
                Address = "Test address",
                TouristLocation = CreateTouristLocation(),
                Stars = 0,
                PricePerNight = 200,
                Available = true,
                Telephone = "0999979",
                InformationText = "Test information text"
            };

            entity.ValidateEntity();
        }
        
        private TouristLocation CreateTouristLocation()
        {
            TouristLocation touristLocation = new TouristLocation()
            {
                Id = Guid.NewGuid()
            };

            return touristLocation;
        }

        private Lodging CreateLodging()
        {
            var lodging = new Lodging()
            {
                Id = Guid.NewGuid(),
                Name = "Test Name",
                Description = "Test Description",
                Address = "Test address",
                TouristLocation = CreateTouristLocation(),
                Stars = 3,
                PricePerNight = 200,
                Available = true,
                Telephone = "0999979",
                InformationText = "Test information text"
            };
            return lodging;
        }

    }
}
