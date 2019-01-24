using BaseModel;
using Model.Mappers;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace Model.Model
{
    public class AssemblyMetadata
    {
        public string Name { get; set; }
        public List<NamespaceMetadata> Namespaces { get; set; }       

        public AssemblyMetadata(Assembly assembly)
        {
            this.Name = assembly.ManifestModule.Name;
            this.Namespaces = (from Type _type in assembly.GetTypes()
                           group _type by _type.GetNamespace() into _group
                           orderby _group.Key
                           select new NamespaceMetadata(_group.Key, _group.ToList())).ToList();
        }

        public AssemblyMetadata() { }


        public void Save(string path, Logic logic)
        {
            logic.Serializer.Save(path, MapperAssembly.MapDown(this, logic.baseAssembly.GetType()));
        }

        public AssemblyMetadata Load(string path, Logic logic)
        {
            Console.WriteLine("Load");
            return MapperAssembly.MapUp(logic.Serializer.Read(path));
        }
    }
}
