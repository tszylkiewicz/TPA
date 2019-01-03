using BaseModel;
using BaseModel.Enums;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace XMLSerializer.XMLModel
{
    public  class XMLMethod : BaseMethod
    {
        public override string Name { get; set; }
        public new List<XMLType> GenericArguments { get; set; }
        public override Tuple<BaseAccessLevel, BaseAbstractEnum, BaseStaticEnum, BaseVirtualEnum> Modifiers { get; set; }
        public new XMLType ReturnType { get; set; }
        public override bool Extension { get; set; }
        public new List<XMLParameter> Parameters { get; set; }
    }
}
