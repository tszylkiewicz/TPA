using BaseModel;
using Newtonsoft.Json;
using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using XMLData.XMLModel;

namespace XMLData
{
    [Export(typeof(ISerializer))]
    public class XMLData : ISerializer
    {
        public void Save(string path, BaseAssembly obj)
        {
            XMLAssembly xmlMetadata = (XMLAssembly)obj;
            string name = JsonConvert.SerializeObject(xmlMetadata, Newtonsoft.Json.Formatting.Indented,
                new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });

            Console.WriteLine(name);
            XNode node = JsonConvert.DeserializeXNode(name, "XMLAssembly");
            XDocument xDocument = node.Document;
            xDocument.Save(path);
        }


        public BaseAssembly Read(string path)
        {
            using (StreamReader file = new StreamReader(path, true))
            {
                string reader = file.ReadToEnd();
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(reader);
                string json = JsonConvert.SerializeXmlNode(doc);
                json = json.Remove(0, 61);
                json = json.Remove(json.Length - 1);
                Console.WriteLine(json);
                XMLAssembly xmlAssembly = JsonConvert.DeserializeObject<XMLAssembly>(json,
                new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
                Console.WriteLine("asd" + xmlAssembly.Name);
                return xmlAssembly;
            }
        }
    }
}
