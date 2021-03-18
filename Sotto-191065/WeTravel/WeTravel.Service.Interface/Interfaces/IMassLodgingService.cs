using WeTravel.Domain;
using System.Collections.Generic;
using WeTravel.Model;
using System;

namespace WeTravel.ServiceInterface
{
    public interface IMassLodgingService
    {
        IEnumerable<LodgingMassImpsModelOut> GetImplementationsForMassAdd();
        void MassCreate(int implementation, string resourceLocation);
    }
}

