using Model.Logger;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model
{
    [DataContract(IsReference = true)]
    public class NamespaceMetadata
    {
        private LogWriter logWriter;
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public List<TypeMetadata> Types { get; set; }

        public NamespaceMetadata(string name, List<Type> types)
        {
            this.Name = name;
            this.Types = (from type in types orderby type.Name select new TypeMetadata(type)).ToList();
            this.logWriter = new LogWriter("Utworzono obiekt klasy NamespaceMetadata: " + Name);
        }

        public NamespaceMetadata() { }
    }
}
