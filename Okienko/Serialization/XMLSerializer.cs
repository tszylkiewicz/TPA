using BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XMLSerializer
{
    [Export(typeof(ISerializer))]
    public class XMLSerializer : ISerializer
    {
        public void Serialize<T>(string path, T obj)
        {
            DataContractSerializer dcs =
                new DataContractSerializer(typeof(T));

            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                dcs.WriteObject(fileStream, obj);
            }
        }

        public T Deserialize<T>(string path)
        {
            DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(T));
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                return (T)dataContractSerializer.ReadObject(fileStream);
            }
        }
    }
}
