using WeTravel.Domain;
using System.Collections.Generic;
using WeTravel.Model;
using System;

namespace WeTravel.ServiceInterface
{
    public interface ILodgingService
    {
        void Create(LodgingModelIn model);
        void Delete(Guid id);
        IEnumerable<LodgingModelOut> Get(LodgingModelFilter filter);
        void UpdateAvailable(Guid id);
        LodgingModelOut GetFromId(Guid id);
    }
}

