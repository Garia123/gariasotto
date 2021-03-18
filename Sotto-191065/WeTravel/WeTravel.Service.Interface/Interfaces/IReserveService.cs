using System;
using WeTravel.Model;

namespace WeTravel.ServiceInterface
{
    public interface IReserveService
    {
        ReserveModelOut Create(ReserveModelIn reserveModel);
        ReserveDescriptionModelOut GetById(Guid id);
        void UpdateState(ReserveDescriptionModelIn reserveDescriptionModel);
    }
}

