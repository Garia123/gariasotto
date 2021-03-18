using System;
using System.Collections.Generic;
using WeTravel.Domain;
using WeTravel.Model;

namespace WeTravel.DataAccessInterface
{
    public interface IReserveRepository
    {
        void Create(Reserve reserve);
        Reserve GetById(Guid id);
        void UpdateState(ReserveDescription state);
        IEnumerable<Reserve> GetReserves(TouristLocationReportFilter touristLocationReportFilter);
    }
}
