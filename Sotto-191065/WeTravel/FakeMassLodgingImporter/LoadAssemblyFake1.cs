using System.Collections.Generic;
using MassLodgingImporter;

namespace AssemblyFake
{
    public class LoadAssemblyFake1 : IMassLodgingImporter
    {
        public IEnumerable<LodgingMassLodgingModel> GetElements(string filePath)
        {
            return new List<LodgingMassLodgingModel>()
            {
                new LodgingMassLodgingModel()
                {
                    Name = "Fake Name 1"
                }
            };
        }
    }
}