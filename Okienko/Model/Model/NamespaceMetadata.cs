
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
    public class NamespaceMetadata
    {
        public string Name { get; set; }
        public List<TypeMetadata> Types { get; set; }

        public NamespaceMetadata(string name, List<Type> types)
        {
            this.Name = name;
            this.Types = (from type in types orderby type.Name select new TypeMetadata(type)).ToList();
        }

        public NamespaceMetadata() { }
    }
}
