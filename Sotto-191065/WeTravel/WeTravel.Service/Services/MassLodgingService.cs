using MassLodgingImporter;
using System;
using System.Collections.Generic;
using System.Linq;
using WeTravel.Model;
using WeTravel.ServiceInterface;

namespace WeTravel.Service
{
    public class MassLodgingService
    {
        private readonly ITouristLocationService _touristLocationService;
        private readonly ILoadMassLodgingAssembly _loadMassLodgingAssembly;
        private readonly ILodgingService _lodgingService;

        public MassLodgingService(
            ILodgingService lodgingService,
            ITouristLocationService touristLocationService,
            ILoadMassLodgingAssembly loadMassLodgingAssembly)
        {
            _lodgingService = lodgingService;
            _touristLocationService = touristLocationService;
            _loadMassLodgingAssembly = loadMassLodgingAssembly;
        }

        public IEnumerable<LodgingMassImpsModelOut> GetImplementationsForMassAdd()
        {
            var implementations = _loadMassLodgingAssembly.GetImplementations();
            var implementationsResults = new List<LodgingMassImpsModelOut>();
            
            for (int i = 0; i < implementations.Count(); i++)
            {
                var implementation = implementations.ElementAt(i);
                implementationsResults.Add(new LodgingMassImpsModelOut()
                {
                    Name = implementation.FullName,
                    Index = i,
                });
            }

            return implementationsResults;
        }

        public void MassCreate(int implementation, string filePath)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + "Assemblies";
            var implementationSelected = _loadMassLodgingAssembly.GetImplementation(implementation);
            var lodgings = implementationSelected.GetElements(filePath);
            
            foreach (var lodging in lodgings)
            {
                if (lodging.TouristLocationId == Guid.Empty)
                {
                    _touristLocationService.Create(GetTouristLocationModelIn(lodging.TouristLocationModel));
                    var touristLocationId = _touristLocationService.GetTouristLocations(null).Last().Id;
                    lodging.TouristLocationId = touristLocationId;
                }

                _lodgingService.Create(GetLodgingModelIn(lodging));
            }
        }

        private LodgingModelIn GetLodgingModelIn(LodgingMassLodgingModel massLodgingModel)
        {
            return new LodgingModelIn()
            {
                Name = massLodgingModel.Name,
                Address = massLodgingModel.Address,
                Available = massLodgingModel.Available,
                Images = massLodgingModel.Images,
                Stars = massLodgingModel.Stars,
                InformationText = massLodgingModel.InformationText,
                TouristLocationId = massLodgingModel.TouristLocationId,
                Telephone = massLodgingModel.Telephone,
                PricePerNight = massLodgingModel.PricePerNight,
                Description = massLodgingModel.Description,
            };
        }
        
        private TouristLocationModelIn GetTouristLocationModelIn(TouristLocationMassLodgingModel touristLocationMassLodgingModel)
        {
            return new TouristLocationModelIn()
            {
                Name = touristLocationMassLodgingModel.Name,
                Description = touristLocationMassLodgingModel.Description,
                Image = touristLocationMassLodgingModel.Image,
                RegionId = touristLocationMassLodgingModel.RegionId,
                CategoryIds = touristLocationMassLodgingModel.CategoryIds
            };
        }
    }
}