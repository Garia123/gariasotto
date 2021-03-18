using System.Collections.Generic;
using WeTravel.Domain;
using WeTravel.DataAccessInterface;
using WeTravel.Model;
using System.Linq;
using System;
using WeTravel.Domain.Exceptions;
using WeTravel.ServiceInterface;
using WeTravel.Domain.Entities;

namespace WeTravel.Service
{
    public class LodgingService : ServiceBase, ILodgingService
    {
        private readonly ReservePrice _reservePrice;

        public LodgingService(IUnitOfWork unitOfWork, ReservePrice reservePrice) : base(unitOfWork)
        {
            _reservePrice = reservePrice;
        }

        public virtual void Create(LodgingModelIn model)
        {
            if (model == null)
            {
                throw new ArgumentExceptionBeautifier("Empty");
            }
            var lodging = _getLodgingFrom(model);
            lodging.ValidateEntity();
            UnitOfWork.LodgingRepository.Create(lodging);
            UnitOfWork.Save();
        }

        private Lodging _getLodgingFrom(LodgingModelIn model)
        {
            return new Lodging()
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Description = model.Description,
                Address = model.Address,
                Stars = model.Stars,
                Images = GetImagesFromModel(model),
                PricePerNight = model.PricePerNight,
                Available = true,
                Telephone = model.Telephone,
                InformationText = model.InformationText,
                TouristLocation = getTouristLocationFromId(model),
            };
        }

        private static IEnumerable<Image> GetImagesFromModel(LodgingModelIn model)
        {
            return model.Images != null ? model.Images.Select(m => new Image() { Id = Guid.NewGuid(), ImageData = Convert.FromBase64String(m ?? "") }).ToList() : new List<Image>();
        }

        private TouristLocation getTouristLocationFromId(LodgingModelIn lodginModel)
        {
            return new TouristLocation() { Id = lodginModel.TouristLocationId };
        }

        public IEnumerable<LodgingModelOut> Get(LodgingModelFilter filter)
        {
            LodgingPriceDTO priceCalculator = getPriceCalculator(filter);

            return UnitOfWork.LodgingRepository.Get(filter).Select(l => GetModelFromEntity(l, priceCalculator));
        }
        
        private LodgingPriceDTO getPriceCalculator(LodgingModelFilter filter)
        {
            if (filter != null && (filter.Adults > 0))
            {
                return new LodgingPriceDTO()
                {
                    CheckIn = filter.CheckIn,
                    CheckOut = filter.CheckOut,
                    Adults = filter.Adults,
                    Children = filter.Children,
                    Babies = filter.Babies
                };
            }
            else
            {
                return null;
            }
        }

        private LodgingModelOut GetModelFromEntity(Lodging lodging, LodgingPriceDTO priceCalc)
        {
            var reserveDTO = new ReservePriceDTO()
            {
                LodgingPriceDTO = priceCalc,
                PricePerNight = lodging.PricePerNight
            };

            return new LodgingModelOut()
            {
                Id = lodging.Id,
                Name = lodging.Name,
                Stars = lodging.Stars,
                Description = lodging.Description,
                Address = lodging.Address,
                PricePerNight = lodging.PricePerNight,
                Available = lodging.Available,
                Telephone = lodging.Telephone,
                InformationText = lodging.InformationText,
                TouristLocationId = lodging.TouristLocation.Id,
                TotalPrice = priceCalc == null ? 0 : _reservePrice.TotalPriceOfStay(reserveDTO),
                Images = lodging.Images?.Select(i => Convert.ToBase64String(i.ImageData ?? new byte[0]))?? new List<string>()
            };
        }

        public void Delete(Guid id)
        {
            UnitOfWork.LodgingRepository.Delete(id);
            UnitOfWork.Save();
        }

        public void UpdateAvailable(Guid id)
        {
            UnitOfWork.LodgingRepository.ChangeStatus(id);
            UnitOfWork.Save();
        }

        public LodgingModelOut GetFromId(Guid id)
        {
            return GetModelFromEntity(UnitOfWork.LodgingRepository.GetById(id), null);
        }
    }
}
