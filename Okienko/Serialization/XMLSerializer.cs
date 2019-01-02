using BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using XMLSerializer.XMLModel;

namespace XMLSerializer
{
    [Export(typeof(ISerializer))]
    public class XMLSerializer : ISerializer
    {
        public void Serialize(string path, BaseAssembly obj)
        {
            XMLAssembly assembly = (XMLAssembly)obj;
            DataContractSerializer dcs =
                new DataContractSerializer(typeof(XMLAssembly));
            string name = JsonConvert.SerializeObject(assembly, Formatting.Indented,
                new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
            Console.WriteLine(name);
            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                dcs.WriteObject(fileStream, name);
            }
        }

        public BaseAssembly Deserialize(string path)
        {
            DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(XMLAssembly));


            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                return (XMLAssembly)dataContractSerializer.ReadObject(fileStream);
            }
        }
    }
}
