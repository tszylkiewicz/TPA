using Model.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model
{
    public class NamespaceMetadata
    {
        LogWriter logWriter;
        public string m_NamespaceName;
        public IEnumerable<TypeMetadata> m_Types;
        internal NamespaceMetadata(string name, IEnumerable<Type> types)
        {
            m_NamespaceName = name;
            m_Types = from type in types orderby type.Name select new TypeMetadata(type);
            logWriter = new LogWriter("Utworzono obiekt klasy NamespaceMetadata: " + m_NamespaceName);
        }
    }
}
