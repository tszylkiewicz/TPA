using BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XMLModel
{
    [DataContract(IsReference = true)]
    public class XMLAssembly : BaseAssembly
    {
        [DataMember] public override string Name { get; set; }
        [DataMember] public new List<XMLNamespace> Namespaces { get; set; }
    }
}
