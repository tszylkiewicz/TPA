using Model.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model
{
    public class Reflector
    {
        LogWriter logWriter;
        public AssemblyMetadata m_AssemblyModel { get; private set; }

        public void Reflect(string assemblyFile)
        {
            Assembly assembly = Assembly.LoadFrom(assemblyFile);
            m_AssemblyModel = new AssemblyMetadata(assembly);
            logWriter = new LogWriter("Utworzono obiekt klasy Reflektor");

        }

        public void Reflect(Assembly assembly)
        {
            m_AssemblyModel = new AssemblyMetadata(assembly);
            logWriter = new LogWriter("Utworzono obiekt klasy Reflektor");
        }
    }
}
