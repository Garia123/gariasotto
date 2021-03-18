using System;
using System.Collections.Generic;
using System.Linq;
using WeTravel.DataAccessInterface;
using WeTravel.Domain;
using WeTravel.Domain.Entities;
using WeTravel.Domain.Exceptions;
using WeTravel.Model;
using WeTravel.ServiceInterface;

namespace WeTravel.Service
{
    public class TouristLocationService : ServiceBase, ITouristLocationService
    {
        public TouristLocationService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public void Create(TouristLocationModelIn touristLocationModel)
        {
            if (touristLocationModel == null)
            {
                throw new ArgumentExceptionBeautifier("Empty");
            }

            var tourist = GetTouristLocationFromModel(touristLocationModel);
            tourist.ValidateEntity();
            UnitOfWork.TouristLocationRepository.Create(tourist);
            UnitOfWork.Save();
        }

        private TouristLocation GetTouristLocationFromModel(TouristLocationModelIn touristLocationModel)
        {
            var id = Guid.NewGuid();
            return new TouristLocation()
            {
                Id = id,
                Name = touristLocationModel.Name,
                Description = touristLocationModel.Description,
                Image = new Image(){ImageData = Convert.FromBase64String(touristLocationModel.Image??""), Id = Guid.NewGuid()},
                Region = getRegionFromId(touristLocationModel.RegionId),
                TouristLocationCategories = getTouristLocationFromId(touristLocationModel, id)
            };
        }

        private IEnumerable<TouristLocationCategory> getTouristLocationFromId(TouristLocationModelIn touristLocationModel,Guid id)
        {
            return touristLocationModel.CategoryIds.Select(t => new TouristLocationCategory()
            {
                CategoryId = t,
                TouristLocationId = id
            }).ToList();
        }

        private Region getRegionFromId(Guid regionId)
        {
            return new Region()
            {
                Id = regionId
            };
        }

        public IEnumerable<TouristLocationModelOut> GetTouristLocations(TouristLocationModelFilter expression)
        {
            return UnitOfWork.TouristLocationRepository.Get(expression).Select(t => GetModelFromTouristLocation(t)).ToList();
        }

        private TouristLocationModelOut GetModelFromTouristLocation(TouristLocation touristLocation)
        {
            return new TouristLocationModelOut()
            {
                Id = touristLocation.Id,
                Name = touristLocation.Name,
                Description = touristLocation.Description,
                Image = Convert.ToBase64String(touristLocation.Image?.ImageData ?? new byte[0]),
                RegionId = touristLocation.Region.Id,
                CategoryIds = touristLocation.TouristLocationCategories.Select(t => t.CategoryId).ToList()
            };
        }

        public TouristLocationModelOut GetById(Guid id)
        {
            return GetModelFromTouristLocation(UnitOfWork.TouristLocationRepository.GetById(id));
        }

        public IEnumerable<ReportLineOut> GetReport(TouristLocationReportFilter touristLocationReportFilter)
        {
            ValidateFilter(touristLocationReportFilter);
            UnitOfWork.ReserveRepository.GetReserves(touristLocationReportFilter);
            var reserves = UnitOfWork.ReserveRepository.GetReserves(touristLocationReportFilter);

            return parseReserves(reserves);
        }

        private IEnumerable<ReportLineOut> parseReserves(IEnumerable<Reserve> reserves)
        {
            var reservesMap = new Dictionary<string, int>();
            foreach(var reserve in reserves)
            {
                var lodgingName = reserve.Lodging.Name;
                getReservesMap(reservesMap, lodgingName);
            }

            return parseMapFromReserves(reservesMap);
        }

        private IEnumerable<ReportLineOut> parseMapFromReserves(Dictionary<string, int> reservesMap)
        {
            var result = new List<ReportLineOut>();
            foreach (var item in reservesMap)
            {
                result.Add(new ReportLineOut { LodgingName = item.Key, ReserveQuantities = item.Value });
            }
            return result;
        }

        private static void getReservesMap(Dictionary<string, int> reservesMap, string lodgingName)
        {
            if (!reservesMap.ContainsKey(lodgingName))
            {
                reservesMap.Add(lodgingName, 1);
            }
            else
            {
                reservesMap[lodgingName] = reservesMap[lodgingName] + 1;
            }
        }

        private void ValidateFilter(TouristLocationReportFilter touristLocationReportFilter)
        {
            ValidateName(touristLocationReportFilter);
            ValidateDates(touristLocationReportFilter);
        }

        private void ValidateDates(TouristLocationReportFilter touristLocationReportFilter)
        {
            if(touristLocationReportFilter.StartDate > touristLocationReportFilter.EndDate)
            {
                throw new InvalidOperationExceptionBeautifier("start date small than end date");
            }
        }

        private void ValidateName(TouristLocationReportFilter touristLocationReportFilter)
        {
            if(string.IsNullOrWhiteSpace(touristLocationReportFilter.TouristLocationName))
            {
                throw new ArgumentExceptionBeautifier("TouristLocationName");
            }
        }
    }
}