using WeTravel.Domain;
using System.Collections.Generic;
using WeTravel.Model;
using System;

namespace WeTravel.DataAccessInterface
{
    public interface ILodgingRepository
    {
        void Create(Lodging lodging);
        IEnumerable<Lodging> Get(LodgingModelFilter filter);
        void Delete(Guid guid);
        void ChangeStatus(Guid guid);
        Lodging GetById(Guid guid);
    }
}
