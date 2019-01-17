using BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XMLSerializer.XMLModel
{
    public class XMLNamespace : BaseNamespace
    {
        public override string Name { get; set; }
        public new List<XMLType> Types { get; set; }
    }
}
