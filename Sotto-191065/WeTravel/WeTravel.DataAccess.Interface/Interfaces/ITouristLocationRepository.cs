using WeTravel.Domain;
using System.Collections.Generic;
using System;
using WeTravel.Model;

namespace WeTravel.DataAccessInterface
{
    public interface ITouristLocationRepository
    {
        void Create(TouristLocation touristLocation);
        IEnumerable<TouristLocation> Get(TouristLocationModelFilter filter);
        TouristLocation GetById(Guid id);
    }
}
