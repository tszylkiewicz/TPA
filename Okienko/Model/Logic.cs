using BaseModel;
using Model.Model;
using Model.Model.Mappers;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Logic
    {
        [Import(typeof(ISerializer))]
        public ISerializer Serializer { get; set; }

        [Import(typeof(BaseAssembly))]
        public BaseAssembly AssemblyMetadata { get; set; }

        private string _compositionPath = "../../../plugins";

        public Logic(ISerializer serializer, BaseAssembly dataLayer)
        {
            this.Serializer = serializer;
            this.AssemblyMetadata = dataLayer;
        }

        public Logic()
        {
            Compose();
        }

        public Logic(string CompositionPath)
        {
            _compositionPath = CompositionPath;
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
            Serializer.Serialize<BaseAssembly>(path, MapperAssembly.MapDown(model, AssemblyMetadata.GetType()));
        }

        public AssemblyMetadata Load(string path)
        {
            return MapperAssembly.MapUp(Serializer.Deserialize<BaseAssembly>(path));
        }
    }
}
