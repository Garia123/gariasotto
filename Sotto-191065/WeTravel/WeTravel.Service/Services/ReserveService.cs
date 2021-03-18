using System;
using WeTravel.DataAccessInterface;
using WeTravel.Domain;
using WeTravel.Domain.Exceptions;
using WeTravel.Model;
using WeTravel.ServiceInterface;

namespace WeTravel.Service
{
    public class ReserveService : ServiceBase, IReserveService
    {
        private readonly ReservePrice _reservePrice;

        public ReserveService(IUnitOfWork uow, ReservePrice reservePrice) : base(uow)
        {
            _reservePrice = reservePrice;
        }

        public ReserveModelOut Create(ReserveModelIn reserveModel)
        {
            if (reserveModel == null)
            {
                throw new ArgumentExceptionBeautifier("Empty");
            }
            var reserve = GetReserveFromModel(reserveModel);
            var reserveDescription = CreateReserveDescription(reserve.Id);
            reserve.ReserveDescription = reserveDescription;
            reserve.ValidateEntity();

            UnitOfWork.ReserveRepository.Create(reserve);
            UnitOfWork.Save();

            var reserveModelOut = GetReserveModelOutFromEntity(reserve);

            return reserveModelOut;
        }
        
        public ReserveDescriptionModelOut GetById(Guid id)
        {
            return GetReserveDescriptionModelOutFromEntity(UnitOfWork.ReserveRepository.GetById(id));
        }

        public void UpdateState(ReserveDescriptionModelIn reserveDescriptionModel)
        {
            if(reserveDescriptionModel == null)
            {
                throw new ArgumentExceptionBeautifier("Empty");
            }
            var reserveDescription = GetReserveDescriptionFromModel(reserveDescriptionModel);
            reserveDescription.ValidateEntity();
            UnitOfWork.ReserveRepository.UpdateState(reserveDescription);
            UnitOfWork.Save();
        }

        private Reserve GetReserveFromModel(ReserveModelIn reserveModel)
        {
            var lodging = UnitOfWork.LodgingRepository.GetById(reserveModel.LodgingId);

            var lodgingPriceDto = new LodgingPriceDTO()
            {
                CheckIn = reserveModel.CheckIn,
                CheckOut = reserveModel.CheckOut,
                Adults = reserveModel.Adults,
                Children = reserveModel.Children,
                Babies = reserveModel.Babies,
                Seniors = reserveModel.Seniors
            };

            var reserveDto = new ReservePriceDTO()
            {
                LodgingPriceDTO = lodgingPriceDto,
                PricePerNight = lodging.PricePerNight
            };

            var reserve = new Reserve()
            {
                Id = Guid.NewGuid(),
                CheckIn = reserveModel.CheckIn,
                CheckOut = reserveModel.CheckOut,
                Adults = reserveModel.Adults,
                Children = reserveModel.Children,
                Babies = reserveModel.Babies,
                ContactFirstName = reserveModel.ContactFirstName,
                ContactLastName = reserveModel.ContactLastName,
                ContactEmail = reserveModel.ContactEmail,
                Price = _reservePrice.TotalPriceOfStay(reserveDto),
                Telephone = lodging.Telephone,
                InformationText = lodging.InformationText,
                LodgingId = reserveModel.LodgingId,
            };
            return reserve;
        }

        private ReserveDescription CreateReserveDescription(Guid reserveId)
        {
            var reserveDescription = new ReserveDescription()
            {
                ReserveId = reserveId,
                Description = "The reservation is created and waiting to be pending payment.",
                State = ReserveState.CREATED
            };
            reserveDescription.ValidateEntity();

            return reserveDescription;
        }

        private static ReserveModelOut GetReserveModelOutFromEntity(Reserve reserve)
        {
            return new ReserveModelOut()
            {
                Id = reserve.Id,
                InformationText = reserve.InformationText,
                Telephone = reserve.Telephone
            };
        }

        private static ReserveDescriptionModelOut GetReserveDescriptionModelOutFromEntity(Reserve reserve)
        {
            return new ReserveDescriptionModelOut()
            {
                ReserveNumber = reserve.Id,
                ContactFullName = reserve.ContactFirstName + " " + reserve.ContactLastName,
                Description = reserve.ReserveDescription.Description,
                State = reserve.ReserveDescription.State.ToString()
            };
        }

        private static ReserveDescription GetReserveDescriptionFromModel(ReserveDescriptionModelIn reserveDescriptionModel)
        {
            if (reserveDescriptionModel.State < 0 || reserveDescriptionModel.State > 4)
            {
                throw new InvalidOperationExceptionBeautifier("Invalid state format");
            }

            return new ReserveDescription()
            {
                ReserveId = reserveDescriptionModel.ReserveId,
                State = (ReserveState)reserveDescriptionModel.State,
                Description = reserveDescriptionModel.Description
            };
        }
    }
}
