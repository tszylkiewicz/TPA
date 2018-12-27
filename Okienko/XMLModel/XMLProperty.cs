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
    public class XMLProperty : BaseProperty
    {
        [DataMember] public override string Name { get; set; }
        [DataMember] public new XMLType PropertyType { get; set; }
    }
}
