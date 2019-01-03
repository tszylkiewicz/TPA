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
            //string pathjas = "C:\\Users\\Marcin\\Documents\\GitHub\\asd.json";
            //if (File.Exists(path)) File.Delete(path);


            XMLAssembly xmlMetadata = (XMLAssembly)obj;
            string name = JsonConvert.SerializeObject(xmlMetadata, Newtonsoft.Json.Formatting.Indented,
                new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });

            XNode node = JsonConvert.DeserializeXNode(name, "Root");
            XDocument xDocument = node.Document;
            xDocument.Save(path);

            //using (StreamWriter file = new StreamWriter(pathjas, true))
            //{
            //    file.Write(name);

            //}
        }


        public BaseAssembly Deserialize(string path)
        {
            using (StreamReader file = new StreamReader(path, true))
            {
                string reader = file.ReadToEnd();
                return JsonConvert.DeserializeObject<XMLAssembly>(reader,
                    new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
            }
        }
    }
}
