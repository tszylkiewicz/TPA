using Model.Model;
using Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class SerializeMangager
    {
        public ISerializer Serializer;// = new XMLSerializer();

        public SerializeMangager(ISerializer serializer)
        {
            Serializer = serializer;
        }

        public SerializeMangager()
        {
            Serializer = new XMLSerializer();
        }

        public void SaveToXml(string path, AssemblyMetadata AssemblyModel)
        {
            if (path != null)
            {
                Serializer.Serialize(path, AssemblyModel);
            }
        }

    }
}
