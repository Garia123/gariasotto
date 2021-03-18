using System;
using System.Collections.Generic;
using System.Text;
using WeTravel.Domain;

namespace WeTravel.DataAccessInterface
{
    public interface IRegionRepository
    {
        IEnumerable<Region> Get();
    }
}

