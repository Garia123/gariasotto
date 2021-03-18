
using System;
using System.Collections.Generic;
using System.Linq;
using MassLodgingImporter;
using Newtonsoft.Json.Linq;

namespace JSONVehiclesImporter
{
    public class JsonMassLodgingImporterLogic : IMassLodgingImporter
    {
        public IEnumerable<LodgingMassLodgingModel> GetElements(string filePath)
        {
            var lodgingModels = new List<LodgingMassLodgingModel>();

            if (!string.IsNullOrWhiteSpace(filePath))
            {
                try {
                    var array = JObject.Parse(filePath)["Lodgings"].ToArray();
                    foreach (var item in array)
                    {
                        lodgingModels.Add(GetVehicleFromJSON(item));
                    }
                }
                catch (Exception)
                {
                    return lodgingModels;
                }
            }
            return lodgingModels;
        }
        
        private LodgingMassLodgingModel GetVehicleFromJSON(JToken item)
        {
            string name = (string) item["Name"];
            string address = (string) item["Address"];
            bool available = (bool) item["Available"];
            string description = (string) item["Description"];
            string informationText = (string) item["InformationText"];
            int pricePerNight = (int) item["PricePerNight"];
            int stars = (int) item["Stars"];
            string telephone = (string) item["Telephone"];
            string touristId = (string) item["TouristLocationId"];
            Guid.TryParse(touristId, out var touristLocationId);
            
            var lodgingModel = new LodgingMassLodgingModel()
            {
                Name = name,
                Address = address,
                Available = available,
                Description = description,
                InformationText = informationText,
                PricePerNight = pricePerNight,
                Stars = stars,
                Telephone = telephone,
                TouristLocationId = touristLocationId,
                TouristLocationModel = null,
            };

            if (touristLocationId == Guid.Empty)
            {
                var touristLocationModel = GetTouristLocationMassLodgingModel(item);
            
                lodgingModel.TouristLocationModel = touristLocationModel;
            }

            return lodgingModel;
        }
        
        private static TouristLocationMassLodgingModel GetTouristLocationMassLodgingModel(JToken item)
        {
            string tName = (string) item["TouristLocationModel"]["Name"];
            string tDescription = (string) item["TouristLocationModel"]["Description"];
            string touristLocationId = (string) item["TouristLocationModel"]["RegionId"];
            Guid.TryParse(touristLocationId, out var tRegionId);
            IEnumerable<Guid> tCategoriesId = item["CategoryIds"].Select(c => (Guid)c["Id"]).ToList();
        
            var touristLocationModel = new TouristLocationMassLodgingModel()
            {
                Name = tName,
                CategoryIds = tCategoriesId,
                Description = tDescription,
                RegionId = tRegionId
            };
            
            return touristLocationModel;
        }
    }
}
