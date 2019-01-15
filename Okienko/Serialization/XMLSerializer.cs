using BaseModel;
using Newtonsoft.Json;
using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Xml.Linq;
using XMLSerializer.XMLModel;

namespace XMLSerializer
{
    [Export(typeof(ISerializer))]
    public class XMLSerializer : ISerializer
    {
        public void Serialize(string path, BaseAssembly obj)
        {
            XMLAssembly xmlMetadata = (XMLAssembly)obj;
            string name = JsonConvert.SerializeObject(xmlMetadata, Newtonsoft.Json.Formatting.Indented,
                new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });

            XNode node = JsonConvert.DeserializeXNode(name, "Root");
            XDocument xDocument = node.Document;
            xDocument.Save(path);
        }


        public BaseAssembly Deserialize(string path)
        {
            Console.WriteLine("XML Deserialize");
            using (StreamReader file = new StreamReader(path, true))
            {
                string reader = file.ReadToEnd();
                return JsonConvert.DeserializeObject<XMLAssembly>(reader,
                    new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
            }
        }
    }
}
