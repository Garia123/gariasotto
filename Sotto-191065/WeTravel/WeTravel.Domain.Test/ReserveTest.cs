using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using WeTravel.Domain.Exceptions;

namespace WeTravel.Domain.Test
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ReserveTest
    {
        [TestMethod]
        public void Valid()
        {
            var reserve = CreateReserve();

            reserve.ValidateEntity();
        }

        [TestMethod]
        [ExpectedException(typeof(FormatExceptionBeautifier))]
        public void MissingId()
        {
            Lodging lodging = CreateLodging();
            var lodgingPriceDTO = new LodgingPriceDTO()
            {
                CheckIn = new DateTime(2020, 10, 13),
                CheckOut = new DateTime(2020, 10, 15),
                Adults = 2,
                Children = 0,
                Babies = 0,
            };
            var reserveDTO = new ReservePriceDTO()
            {
                LodgingPriceDTO = lodgingPriceDTO,
                TotalDays = 2,
            };
            var reservePrice = new ReservePriceCalculation();

            var reserve = new Reserve()
            {
                CheckIn = new DateTime(2020, 10, 13),
                CheckOut = new DateTime(2020, 10, 15),
                Adults = 2,
                Children = 0,
                Babies = 0,
                ContactFirstName = "Mark",
                ContactLastName = "Ztruges",
                ContactEmail = "markztruges@hotmail.com",
                Price = reservePrice.TotalPriceOfStay(reserveDTO),
                Telephone = lodging.Telephone,
                InformationText = lodging.InformationText,
                LodgingId = lodging.Id,
            };

            reserve.ValidateEntity();
        }

        [TestMethod]
        [ExpectedException(typeof(FormatExceptionBeautifier))]
        public void CheckInDateHigherCheckOutDate()
        {
            Lodging lodging = CreateLodging();

            var reserve = CreateReserve();
            reserve.CheckIn = new DateTime(2020, 10, 17);

            reserve.ValidateEntity();
        }

        [TestMethod]
        [ExpectedException(typeof(FormatExceptionBeautifier))]
        public void AdultLessThan1()
        {
            Lodging lodging = CreateLodging();

            var reserve = CreateReserve();
            reserve.Adults = 0;

            reserve.ValidateEntity();
        }

        [TestMethod]
        [ExpectedException(typeof(FormatExceptionBeautifier))]
        public void CheldrenAndBabiesLessThan0()
        {
            Lodging lodging = CreateLodging();

            var reserve = CreateReserve();
            reserve.Children = -1;
            reserve.Babies = -1;

            reserve.ValidateEntity();
        }
        
        [TestMethod]
        [ExpectedException(typeof(FormatExceptionBeautifier))]
        public void MissingLodging()
        {
            Lodging lodging = CreateLodging();
            var lodgingPriceDTO = new LodgingPriceDTO()
            {
                CheckIn = new DateTime(2020, 10, 13),
                CheckOut = new DateTime(2020, 10, 15),
                Adults = 2,
                Children = 0,
                Babies = 0,
            };
            var reserveDTO = new ReservePriceDTO()
            {
                LodgingPriceDTO = lodgingPriceDTO,
                TotalDays = 2,
            };
            var reservePrice = new ReservePriceCalculation();

            var reserve = new Reserve()
            {
                Id = Guid.NewGuid(),
                CheckIn = new DateTime(2020, 10, 13),
                CheckOut = new DateTime(2020, 10, 15),
                Adults = 2,
                Children = 0,
                Babies = 0,
                ContactFirstName = "Mark",
                ContactLastName = "Ztruges",
                ContactEmail = "markztruges@hotmail.com",
                Price = reservePrice.TotalPriceOfStay(reserveDTO),
                Telephone = lodging.Telephone,
                InformationText = lodging.InformationText,
                ReserveDescription = CreateReserveDescription()
            };
           

            reserve.ValidateEntity();
        }

        private ReserveDescription CreateReserveDescription()
        {
            ReserveDescription reserveDescription = new ReserveDescription()
            {
                ReserveId = Guid.NewGuid(),
                Description = "Change reserve",
                State = ReserveState.APPROVED
            };

            return reserveDescription;
        }
        public Guid ReserveId { get; set; }
        public string description { get; set; }
        public ReserveState state { get; set; }

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
        private LodgingPriceDTO CreateLodgingPriceDTO()
        {
            LodgingPriceDTO lodgingpriceDTO = new LodgingPriceDTO()
            {
                CheckIn = new DateTime(2020, 10, 13),
                CheckOut = new DateTime(2020, 10, 15),
                Adults = 2,
                Babies = 0,
                Children = 0
            };
            return lodgingpriceDTO;
        }


        private Reserve CreateReserve()
        {
            Lodging lodging = CreateLodging();
            var lodgingPriceDTO = new LodgingPriceDTO()
            {
                CheckIn = new DateTime(2020, 10, 13),
                CheckOut = new DateTime(2020, 10, 15),
                Adults = 2,
                Children = 0,
                Babies = 0,
            };
            var reserveDTO = new ReservePriceDTO()
            {
                LodgingPriceDTO = lodgingPriceDTO,
                TotalDays = 2,
            };
            var reservePrice = new ReservePriceCalculation();

            var reserve = new Reserve()
            {
                Id = Guid.NewGuid(),
                CheckIn = new DateTime(2020, 10, 13),
                CheckOut = new DateTime(2020, 10, 15),
                Adults = 2,
                Children = 0,
                Babies = 0,
                ContactFirstName = "Mark",
                ContactLastName = "Ztruges",
                ContactEmail = "markztruges@hotmail.com",
                Price = reservePrice.TotalPriceOfStay(reserveDTO),
                Telephone = lodging.Telephone,
                InformationText = lodging.InformationText,
                LodgingId = lodging.Id,
                ReserveDescription = CreateReserveDescription()
            };
            return reserve;
        }
    }
}

