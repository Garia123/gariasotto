
namespace WeTravel.Domain
{
    public class ReservePriceCalculation : ReservePrice
    {
        public override int CalculateTotalForAdults(ReservePriceDTO reservePrice)
        {
            return reservePrice.TotalDays * reservePrice.PricePerNight * reservePrice.LodgingPriceDTO.Adults;
        }

        public override int CalculateTotalForChildren(ReservePriceDTO reservePrice)
        {
            return reservePrice.TotalDays * reservePrice.PricePerNight * reservePrice.LodgingPriceDTO.Children;
        }

        public override int CalculateTotalForBabies(ReservePriceDTO reservePrice)
        {
            return reservePrice.TotalDays * reservePrice.PricePerNight * reservePrice.LodgingPriceDTO.Babies;
        }

        public override int CalculateTotalForSeniors(ReservePriceDTO reservePrice)
        {
            var totalPrice = reservePrice.TotalDays * reservePrice.PricePerNight * reservePrice.LodgingPriceDTO.Seniors;
            return (int)(totalPrice - (totalPrice/2*0.3));
        }
    }
}
