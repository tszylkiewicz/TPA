using Model.Logger;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace Model.Model
{
    [DataContract]
    public class AssemblyMetadata
    {
        [Key]
        public int idAssemby { get; set; }
        [NotMapped]
        private LogWriter logWriter;
        [DataMember]
        public string Name { get; set; }
        //[ForeignKey]
        [DataMember]
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
