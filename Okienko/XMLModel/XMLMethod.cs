using BaseModel;
using BaseModel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XMLModel
{
    [DataContract(IsReference = true)]
    public class XMLMethod : BaseMethod
    {
        [DataMember] public override string Name { get; set; }
        [DataMember] public new List<XMLType> GenericArguments { get; set; }
        [DataMember] public override Tuple<BaseAccessLevel, BaseAbstractEnum, BaseStaticEnum, BaseVirtualEnum> Modifiers { get; set; }
        [DataMember] public new XMLType ReturnType { get; set; }
        [DataMember] public override bool Extension { get; set; }
        [DataMember] public new List<XMLParameter> Parameters { get; set; }
    }
}
