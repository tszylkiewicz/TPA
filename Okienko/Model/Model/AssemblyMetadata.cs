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
        private LogWriter logWriter;
        public string Name { get; set; }
        public List<NamespaceMetadata> Namespaces { get; set; }

        public AssemblyMetadata(Assembly assembly)
        {
            this.Name = assembly.ManifestModule.Name;
            this.Namespaces = (from Type _type in assembly.GetTypes()
                           where _type.GetVisible()
                           group _type by _type.GetNamespace() into _group
                           orderby _group.Key
                           select new NamespaceMetadata(_group.Key, _group.ToList())).ToList();
            this.logWriter = new LogWriter("Utworzono obiekt klasy AssemblyMetadata: " + Name);
        }
    }
}
