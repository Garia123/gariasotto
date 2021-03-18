using WeTravel.Domain;
using System.Collections.Generic;
using WeTravel.Model;
using System;

namespace WeTravel.ServiceInterface
{
    public interface ITouristLocationService
    {
        void Create(TouristLocationModelIn touristLocationModel);
        IEnumerable<TouristLocationModelOut> GetTouristLocations(TouristLocationModelFilter expression);
        TouristLocationModelOut GetById(Guid id);
        IEnumerable<ReportLineOut> GetReport(TouristLocationReportFilter touristLocationReportFilter);
    }
}
