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
    public  class XMLType : BaseType
    {
        public override string Name { get; set; }
        public override string NamespaceName { get; set; }
        public new XMLType BaseTyp { get; set; }
        public new List<XMLType> GenericArguments { get; set; }
        public override Tuple<BaseAccessLevel, BaseSealedEnum, BaseAbstractEnum, BaseStaticEnum> Modifiers { get; set; }
        public override BaseTypeKind TypeKind { get; set; }
        public new List<XMLType> ImplementedInterfaces { get; set; }
        public new List<XMLType> NestedTypes { get; set; }
        public new List<XMLProperty> Properties { get; set; }
        public new XMLType DeclaringType { get; set; }
        public new List<XMLMethod> Methods { get; set; }
        public new List<XMLMethod> Constructors { get; set; }
        public new List<XMLParameter> Attributes { get; set; }
    }
}
