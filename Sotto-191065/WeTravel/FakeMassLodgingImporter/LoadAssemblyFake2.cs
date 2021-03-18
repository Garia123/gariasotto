using System.Collections.Generic;
using MassLodgingImporter;

namespace AssemblyFake
{
    public class LoadAssemblyFake2 : IMassLodgingImporter
    {
        public IEnumerable<LodgingMassLodgingModel> GetElements(string filePath)
        {
            return new List<LodgingMassLodgingModel>()
            {
                new LodgingMassLodgingModel()
                {
                    Name = "Fake Name 2"
                }
            };
        }
    }
}