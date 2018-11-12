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
        private LogWriter logWriter;
        public string Name { get; set; }
        public List<TypeMetadata> Types { get; set; }

        public NamespaceMetadata(string name, List<Type> types)
        {
            this.Name = name;
            this.Types = (from type in types orderby type.Name select new TypeMetadata(type)).ToList();
            this.logWriter = new LogWriter("Utworzono obiekt klasy NamespaceMetadata: " + Name);
        }
    }
}
