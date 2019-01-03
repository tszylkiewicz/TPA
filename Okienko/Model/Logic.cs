using BaseModel;
using Model.Mappers;
using Model.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace Model
{
    public class Logic
    {
        [Import(typeof(ISerializer))]
        public ISerializer Serializer { get; set; }

        [Import(typeof(BaseAssembly))]
        public BaseAssembly baseAssembly { get; set; }

        private string _compositionPath = @"..\\..\\..\\plugins";

        public Logic()
        {
            Compose();
        }

        private void Compose()
        {
            var catalog = new AggregateCatalog(new DirectoryCatalog(_compositionPath));
            var _container = new CompositionContainer(catalog);
            try
            {
                _container.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                throw compositionException;
            }
        }

        public void Save(AssemblyMetadata model, string path)
        {
            Serializer.Serialize(path, MapperAssembly.MapDown(model, baseAssembly.GetType()));
        }

        public AssemblyMetadata Load(string path)
        {
            return MapperAssembly.MapUp(Serializer.Deserialize(path));
        }
    }
}
