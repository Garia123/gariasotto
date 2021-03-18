using System;
using System.Collections.Generic;
using System.Linq;
using AssemblyFake;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeTravel.Domain.Exceptions;

namespace WeTravel.Service.Test
{
    [TestClass]
    public class LoadAssemblyTest
    {
        private LoadMassLodgingAssembly _assemblyLoader;

        [TestInitialize]
        public void Init()
        {
            _assemblyLoader = new LoadMassLodgingAssembly(AppDomain.CurrentDomain.BaseDirectory + "Assemblies");
            Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory + "Assemblies");
        }
        
        [TestMethod]
        public void GetImplementationOk()
        {
            var expectedResult = typeof(LoadAssemblyFake1).Name;
            
            var implementation = _assemblyLoader.GetImplementation(0).GetType().Name;

            Assert.IsTrue(expectedResult.Equals(implementation));
        }
        
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationExceptionBeautifier))]
        public void GetImplementationInvalid()
        {
            var expectedResult = typeof(LoadAssemblyFake1).Name;
            
            var implementation = _assemblyLoader.GetImplementation(20).GetType().Name;

            Assert.IsTrue(expectedResult.Equals(implementation));
        }

        [TestMethod]
        public void GetImplementations()
        {
            var expectedResult = new List<string>()
            {
                typeof(LoadAssemblyFake1).Name,
                typeof(LoadAssemblyFake2).Name,
            };

            var implementations = _assemblyLoader.GetImplementations().Select(i => i.Name).ToList();
            
            Assert.IsTrue(implementations.SequenceEqual(expectedResult));
        }
    }
}