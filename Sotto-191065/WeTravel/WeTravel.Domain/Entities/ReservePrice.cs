using System;
using System.Collections.Generic;
using System.Text;
using WeTravel.Domain.Exceptions;

namespace WeTravel.Domain
{
    public abstract class ReservePrice
    {
        public int TotalPriceOfStay(ReservePriceDTO reservePrice)
        {
            int totalValue = 0;

            ValidateDates(reservePrice.LodgingPriceDTO);
            ValidateGuests(reservePrice.LodgingPriceDTO);

            reservePrice.TotalDays = Convert.ToInt32((reservePrice.LodgingPriceDTO.CheckOut - reservePrice.LodgingPriceDTO.CheckIn).TotalDays);
            totalValue += CalculateTotalForAdults(reservePrice);
            totalValue += CalculateTotalForChildren(reservePrice);
            totalValue += CalculateTotalForBabies(reservePrice);
            totalValue += CalculateTotalForSeniors(reservePrice);

            return totalValue;
        }

        private void ValidateLodgingPriceDTO(LodgingPriceDTO lodgingPrice)
        {
            ValidateDates(lodgingPrice);
            ValidateGuests(lodgingPrice);
        }

        private void ValidateDates(LodgingPriceDTO lodgingPrice)
        {
            if (lodgingPrice.CheckIn >= lodgingPrice.CheckOut)
            {
                throw new FormatExceptionBeautifier("CHECK_IN_DATE");
            }
        }

        private void ValidateGuests(LodgingPriceDTO lodgingPrice)
        {
            if (lodgingPrice.Adults <= 0 || lodgingPrice.Children < 0 || lodgingPrice.Babies < 0 || lodgingPrice.Seniors < 0)
            {
                throw new FormatExceptionBeautifier("GUESTS");
            }
        }

        public abstract int CalculateTotalForAdults(ReservePriceDTO reservePrice);
        public abstract int CalculateTotalForChildren(ReservePriceDTO reservePrice);
        public abstract int CalculateTotalForBabies(ReservePriceDTO reservePrice);
        public abstract int CalculateTotalForSeniors(ReservePriceDTO reservePrice);
    }
}
