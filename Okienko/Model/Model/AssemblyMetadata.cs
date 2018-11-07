using Model.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model
{
    public class AssemblyMetadata
    {
        LogWriter logWriter;
        public string m_Name;
        private IEnumerable<NamespaceMetadata> m_Namespaces;
        private IEnumerable<TypeMetadata> types;

        public IEnumerable<TypeMetadata> Types { get => types; set => types = value; }
        public IEnumerable<NamespaceMetadata> Namespaces { get => m_Namespaces; set => m_Namespaces = value; }
        internal AssemblyMetadata(Assembly assembly)
        {
            m_Name = assembly.ManifestModule.Name;
            m_Namespaces = from Type _type in assembly.GetTypes()
                           //where _type.GetVisible()
                           group _type by _type.GetNamespace() into _group
                           orderby _group.Key
                           select new NamespaceMetadata(_group.Key, _group);
            types = from Type _type in assembly.GetTypes()
                        select new TypeMetadata(_type);
            logWriter = new LogWriter("Utworzono obiekt klasy AssemblyMetadata: " + m_Name);
        }
    }
}
