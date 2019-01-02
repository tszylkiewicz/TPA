using BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XMLSerializer.XMLModel
{
    [DataContract(IsReference = true)]
    public class XMLParameter : BaseParameter
    {
        [DataMember] public override string Name { get; set; }
        [DataMember] public new XMLType Type { get; set; }
    }
}
