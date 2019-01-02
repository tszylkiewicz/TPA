using BaseModel;
using BaseModel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XMLSerializer.XMLModel
{
    [DataContract(IsReference = true)]
    public class XMLType : BaseType
    {
        [DataMember] public override string Name { get; set; }
        [DataMember] public override string NamespaceName { get; set; }
        [DataMember] public new XMLType BaseTyp { get; set; }
        [DataMember] public new List<XMLType> GenericArguments { get; set; }
        [DataMember] public override Tuple<BaseAccessLevel, BaseSealedEnum, BaseAbstractEnum, BaseStaticEnum> Modifiers { get; set; }
        [DataMember] public override BaseTypeKind TypeKind { get; set; }
        [DataMember] public new List<XMLType> ImplementedInterfaces { get; set; }
        [DataMember] public new List<XMLType> NestedTypes { get; set; }
        [DataMember] public new List<XMLProperty> Properties { get; set; }
        [DataMember] public new XMLType DeclaringType { get; set; }
        [DataMember] public new List<XMLMethod> Methods { get; set; }
        [DataMember] public new List<XMLMethod> Constructors { get; set; }
        [DataMember] public new List<XMLParameter> Attributes { get; set; }
    }
}
