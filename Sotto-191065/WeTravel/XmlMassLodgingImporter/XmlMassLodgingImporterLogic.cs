using System;
using System.Collections.Generic;
using System.Xml;
using MassLodgingImporter;

namespace XMLVehiclesImporter
{
    public class XmlMassLodgingImporterLogic : IMassLodgingImporter
    {
        public IEnumerable<LodgingMassLodgingModel> GetElements(string filePath)
        {
            var lodgingModels = new List<LodgingMassLodgingModel>();
            
            if (!string.IsNullOrWhiteSpace(filePath))
            {
                var doc = new XmlDocument();
                
                try {
                    doc.Load(filePath);
                }
                catch (Exception)
                {
                    return lodgingModels;
                }
                
                var xmlNodeList = doc.SelectNodes("/Lodgings/Lodging");
                foreach (XmlNode node in xmlNodeList)
                {
                    try
                    {
                        lodgingModels.Add(GetLodgingModel(node));
                    }
                    catch (NullReferenceException)
                    {
                    }
                }
            }
            
            return lodgingModels;
        }
        
        private LodgingMassLodgingModel GetLodgingModel(XmlNode node)
        {
            var name = node["Name"]?.InnerText;
            var address = node["Address"]?.InnerText;
            var available = Convert.ToBoolean(node["Available"]?.InnerText);
            var description = node["Description"]?.InnerText;
            var informationText = node["InformationText"]?.InnerText;
            var pricePerNight = Convert.ToInt32(node["PricePerNight"]?.InnerText);
            var stars = Convert.ToInt32(node["Stars"]?.InnerText);
            var telephone = node["Telephone"]?.InnerText;
            Guid.TryParse(node["TouristLocationId"]?.InnerText, out var touristLocationId);
            
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
                var touristLocationModel = GetTouristLocationMassLodgingModel(node);

                lodgingModel.TouristLocationModel = touristLocationModel;
            }

            return lodgingModel;
        }

        private static TouristLocationMassLodgingModel GetTouristLocationMassLodgingModel(XmlNode node)
        {
            var tName = node["TouristLocationModel"]?["Name"]?.InnerText;
            var tDescription = node["TouristLocationModel"]?["Description"]?.InnerText;
            Guid.TryParse(node["TouristLocationModel"]?["RegionId"]?.InnerText, out var tRegionId);
            var tCategories = node["TouristLocationModel"].SelectNodes("/categories/category");
            var tCategoriesId = new List<Guid>();

            foreach (XmlNode category in tCategories)
            {
                Guid.TryParse(category["Id"]?.InnerText, out var categoryId);
                tCategoriesId.Add(categoryId);
            }

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