using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using WeTravel.Domain;
using WeTravel.Domain.Exceptions;

namespace WeTravel.Service.Test
{
    [TestClass]
    public class ReservePriceCalculator
    {
        private Lodging CreateLodging()
        {
            return new Lodging()
            {
                Id = Guid.NewGuid(),
                Name = "Test Name",
                Description = "Test Description",
                Address = "Test address",
                TouristLocation = new TouristLocation()
                {
                    Id = Guid.NewGuid()
                },
                Stars = 3,
                PricePerNight = 200,
                Available = true,
                Telephone = "0999979",
                InformationText = "Test information text"
            };
        }

        [TestMethod]
        public void TotalPriceOfStay()
        {
            Lodging lodging = CreateLodging();
            var lodgingPriceDTO = new LodgingPriceDTO()
            {
                CheckIn = new DateTime(2020, 10, 13),
                CheckOut = new DateTime(2020, 10, 15),
                Adults = 2,
                Babies = 0,
                Children = 0
            };
            int totalPrice = 2 * 200 * 2;
            var reserveDTO = new ReservePriceDTO()
            {
                LodgingPriceDTO = lodgingPriceDTO,
                PricePerNight = 200
            };
            var reserve = new ReservePriceCalculation();

            int totalPriceOfStay = reserve.TotalPriceOfStay(reserveDTO);

            Assert.AreEqual(totalPrice, totalPriceOfStay);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatExceptionBeautifier))]
        public void CheckInDateHigherCheckOutDate()
        {
            Lodging lodging = CreateLodging();

            var lodgingPriceDTO = new LodgingPriceDTO()
            {
                CheckIn = new DateTime(2020, 10, 17),
                CheckOut = new DateTime(2020, 10, 15),
                Adults = 2,
                Babies = 0,
                Children = 0
            };

            var reserveDTO = new ReservePriceDTO()
            {
                LodgingPriceDTO = lodgingPriceDTO,
                PricePerNight = 200
            };
            var reserve = new ReservePriceCalculation();

            reserve.TotalPriceOfStay(reserveDTO);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatExceptionBeautifier))]
        public void AdultLessThan1()
        {
            Lodging lodging = CreateLodging();

            var lodgingPriceDTO = new LodgingPriceDTO()
            {
                CheckIn = new DateTime(2020, 10, 13),
                CheckOut = new DateTime(2020, 10, 15),
                Adults = 0,
                Babies = 0,
                Children = 0
            };
            var reserveDTO = new ReservePriceDTO()
            {
                LodgingPriceDTO = lodgingPriceDTO,
                PricePerNight = 200
            };
            var reserve = new ReservePriceCalculation();

            reserve.TotalPriceOfStay(reserveDTO);
        }

    }
}
