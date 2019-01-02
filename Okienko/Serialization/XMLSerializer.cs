using BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using XMLModel;

namespace XMLSerializer
{
    [Export(typeof(ISerializer<XMLAssembly>))]
    public class XMLSerializer : ISerializer<XMLAssembly>
    {
        public void Serialize(string path, XMLAssembly obj)
        {
            DataContractSerializer dcs =
                new DataContractSerializer(typeof(XMLAssembly));

            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                dcs.WriteObject(fileStream, obj);
            }
        }

        public XMLAssembly Deserialize(string path)
        {
            DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(XMLAssembly));
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                return (XMLAssembly)dataContractSerializer.ReadObject(fileStream);               
            }
        }
    }
}
