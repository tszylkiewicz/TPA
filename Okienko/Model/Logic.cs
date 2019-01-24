using BaseModel;
using Model.Mappers;
using Model.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;

namespace Model
{
    public class Logic
    {
        [Import(typeof(ISerializer))]
        public ISerializer Serializer { get; set; }

        [Import(typeof(BaseAssembly))]
        public BaseAssembly baseAssembly { get; set; }

        private string _compositionPath = ConfigurationManager.AppSettings["pluginsPath"];

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
    }
}
