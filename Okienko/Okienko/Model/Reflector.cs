using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Okienko.Model
{
    public class Reflector
    {
        public AssemblyMetadata m_AssemblyModel { get; private set; }

        public void Reflect(string assemblyFile)
        {
            Assembly assembly = Assembly.LoadFrom(assemblyFile);
            m_AssemblyModel = new AssemblyMetadata(assembly);
        }

        public void Reflect(Assembly assembly)
        {
            m_AssemblyModel = new AssemblyMetadata(assembly);
        }
    }
}
