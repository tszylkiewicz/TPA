using System;
using System.Collections.Generic;
using BaseModel.Enums;
using System.Runtime.Serialization;

namespace BaseModel
{
    [DataContract (IsReference = true)]
    public abstract class BaseMethod
    {
        [DataMember] public virtual string Name { get; set; }
        public virtual List<BaseType> GenericArguments { get; set; }
        [DataMember] public virtual Tuple<BaseAccessLevel, BaseAbstractEnum, BaseStaticEnum, BaseVirtualEnum> Modifiers { get; set; }
        public virtual BaseType ReturnType { get; set; }
        [DataMember] public virtual bool Extension { get; set; }
        public virtual List<BaseParameter> Parameters { get; set; }
    }
}
