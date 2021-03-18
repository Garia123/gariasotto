using System.Collections.Generic;

namespace MassLodgingImporter
{
    public interface IMassLodgingImporter
    {
        IEnumerable<LodgingMassLodgingModel> GetElements(string filePath);
    }
}