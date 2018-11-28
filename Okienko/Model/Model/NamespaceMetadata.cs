using Model.Logger;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model
{
    public class NamespaceMetadata
    {
        [Key]
        public int idNamespace { get; set; }
        [NotMapped]
        private LogWriter logWriter;
        public string Name { get; set; }
        public List<TypeMetadata> Types { get; set; }

        [ForeignKey("AssemblyMetadata")]
        public int idAssemby { get; set; }
        public AssemblyMetadata AssemblyMetadata { get; set; }

        public NamespaceMetadata(string name, List<Type> types)
        {
            this.Name = name;
            this.Types = (from type in types orderby type.Name select new TypeMetadata(type)).ToList();
            this.logWriter = new LogWriter("Utworzono obiekt klasy NamespaceMetadata: " + Name);
        }
    }
}
