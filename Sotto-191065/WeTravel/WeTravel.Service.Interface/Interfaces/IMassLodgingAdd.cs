using System.Collections.Generic;
using WeTravel.Model;

namespace WeTravel.ServiceInterface
{
    public interface IMassLodgingAdd
    {
        IEnumerable<LodgingModelIn> GetElements(string filePath);
    }
}