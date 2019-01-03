using Composition.Logger;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class Reflector
    {
        LogWriter logWriter;
        public AssemblyMetadata AssemblyModel { get; private set; }

        public void Reflect(string assemblyFile)
        {
            Assembly assembly = Assembly.LoadFrom(assemblyFile);
            AssemblyModel = new AssemblyMetadata(assembly);
            logWriter = new LogWriter("Utworzono obiekt klasy Reflektor");

        }

        public void Reflect(Assembly assembly)
        {
            AssemblyModel = new AssemblyMetadata(assembly);
            logWriter = new LogWriter("Utworzono obiekt klasy Reflektor");
        }
    }
}
