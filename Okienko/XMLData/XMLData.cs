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
            XDocument node = JsonConvert.DeserializeXNode(name, "Root", true);
            node.Save(path);
        }


        public BaseAssembly Read(string path)
        {
            XDocument doc = XDocument.Load(path);
            string json = JsonConvert.SerializeXNode(doc, Newtonsoft.Json.Formatting.Indented, true);
            json = json.Remove(0, 58);
            return JsonConvert.DeserializeObject<XMLAssembly>(json,
                new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
        }
    }
}
