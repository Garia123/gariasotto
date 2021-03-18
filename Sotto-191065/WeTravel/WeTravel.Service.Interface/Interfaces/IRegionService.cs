using System;
using System.Collections.Generic;
using System.Text;
using WeTravel.Model;

namespace WeTravel.ServiceInterface
{
    public interface IRegionService
    {
        IEnumerable<RegionModelOut> Get();
    }
}

