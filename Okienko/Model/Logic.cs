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
using XMLModel;

namespace Model
{
    public class Logic
    {
        [Import(typeof(ISerializer<BaseAssembly>))]
        public ISerializer<BaseAssembly> Serializer { get; set; }

        [Import(typeof(BaseAssembly))]
        public BaseAssembly AssemblyMetadata { get; set; }

        private string _compositionPath = "../../../plugins";

        public Logic(ISerializer<BaseAssembly> serializer, BaseAssembly dataLayer)
        {
            this.Serializer = serializer;
            this.AssemblyMetadata = dataLayer;
        }

        public Logic()
        {
            //Compose();
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
            ISerializer<XMLAssembly> SerializerTemp = new XMLSerializer.XMLSerializer();
            XMLAssembly ass = new XMLAssembly();
            BaseAssembly temp = MapperAssembly.MapDown(model, ass.GetType());
            XMLAssembly temp2 = (XMLAssembly)temp;
            SerializerTemp.Serialize(path, temp2);
            Console.WriteLine(model.Name);
            foreach(NamespaceMetadata obj in model.Namespaces)
            {
                Console.WriteLine(obj.Name);
            }
            Console.WriteLine(temp.Name);
            foreach(XMLNamespace obj in temp.Namespaces)
            {
                Console.WriteLine(obj.Name);
            }
        }

        public AssemblyMetadata Load(string path)
        {
            return MapperAssembly.MapUp(Serializer.Deserialize(path));
        }
    }
}
