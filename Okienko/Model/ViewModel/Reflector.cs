
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
        public AssemblyMetadata AssemblyModel { get; set; }

        public void Reflect(string assemblyFile)
        {
            Assembly assembly = Assembly.ReflectionOnlyLoadFrom(assemblyFile);
            AssemblyModel = new AssemblyMetadata(assembly);
        }

        public void Reflect(Assembly assembly)
        {
            AssemblyModel = new AssemblyMetadata(assembly);
        }
    }
}
