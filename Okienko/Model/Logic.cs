using BaseModel;
using Model.Mappers;
using Model.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace Model
{
    [Export(typeof(Logic))]
    public class Logic
    {
        //[ImportMany(typeof(ISerializer))]
        //public ISerializer Serializer { get; set; }

        [Import(typeof(BaseAssembly))]
        public BaseAssembly baseAssembly { get; set; }
        //public BaseAssembly baseAssembly = Composition.Compose.Instance._container.GetExportedValue<BaseAssembly>();
        public ISerializer Serializer = Composition.Compose.Instance._container.GetExportedValue<ISerializer>();

        public void Save(AssemblyMetadata model, string path)
        {
            Console.WriteLine("przed Save");
            Serializer.Serialize(path, MapperAssembly.MapDown(model, baseAssembly.GetType()));
        }

        public AssemblyMetadata Load(string path)
        {
            return MapperAssembly.MapUp(Serializer.Deserialize(path));
        }
    }
}
