using MassLodgingImporter;
using System;
using System.Collections.Generic;

namespace WeTravel.ServiceInterface
{
    public interface ILoadMassLodgingAssembly
    {
        IEnumerable<Type> GetImplementations();
        IMassLodgingImporter GetImplementation(int index);
    }
}