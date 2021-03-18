using MassLodgingImporter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using WeTravel.Domain.Exceptions;
using WeTravel.ServiceInterface;

namespace WeTravel.Service
{
    public class LoadMassLodgingAssembly : ILoadMassLodgingAssembly
    {
        private readonly DirectoryInfo directory;
        private IEnumerable<Type> implementations;

        public LoadMassLodgingAssembly(string path)
        {
            this.directory = new DirectoryInfo(path);
            this.implementations = new List<Type>();
            LoadImplementations();
        }

        public IEnumerable<Type> GetImplementations()
        {
            LoadImplementations();
            return this.implementations;
        }

        public IMassLodgingImporter GetImplementation(int index)
        {
            LoadImplementations();
            
            if (this.implementations.ToArray().Length <= index || index <0)
            {
                throw new InvalidOperationExceptionBeautifier("No implementation at index");
            }
            
            return Activator.CreateInstance(this.implementations.ElementAt(index)) as IMassLodgingImporter;
        }

        private void LoadImplementations()
        {
            var files = this.directory.GetFiles("*.dll");
            foreach (var file in files)
            {
                var assemblyLoaded = Assembly.LoadFile(file.FullName);
                var loadedImplementations = assemblyLoaded.GetTypes()
                    .Where(t => typeof(IMassLodgingImporter).IsAssignableFrom(t) && t.IsClass);

                this.implementations = this.implementations.Union(loadedImplementations);
            }
        }
    }
}