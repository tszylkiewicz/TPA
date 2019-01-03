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
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Schema;

namespace XMLSerializer
{
    [Export(typeof(ISerializer))]
    public class XMLSerializer : ISerializer
    {
        public void Serialize(string path, BaseAssembly obj)
        {
            //path = "D:\\Repository\\TPA\\Okienko\\DataToTest\\bin\\Debug\\asd.json";
            //if (File.Exists(path)) File.Delete(path);
            XMLAssembly assembly = (XMLAssembly)obj;
            DataContractSerializer dcs = new DataContractSerializer(typeof(XMLAssembly));
            //string name = JsonConvert.SerializeObject(assembly, Formatting.Indented,
                //new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
            //XNode node = JsonConvert.DeserializeXNode(name, "Root");           
            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                dcs.WriteObject(fileStream, assembly);
            }
            //using (StreamWriter file = new StreamWriter(path, true))
            //{
            //    file.Write(name);
            //}
        }

        public BaseAssembly Deserialize(string path)
        {
            XMLAssembly assembly;
            DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(XMLAssembly));
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                assembly = (XMLAssembly)dataContractSerializer.ReadObject(fileStream);
            }
            return assembly;
        }
    }
}
