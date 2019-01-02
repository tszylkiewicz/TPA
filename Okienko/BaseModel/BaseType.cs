﻿using BaseModel.Enums;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BaseModel
{
    [DataContract (IsReference = true)]
    public abstract class BaseType
    {
        [DataMember] public virtual string Name { get; set; }
        public virtual string NamespaceName { get; set; }
        public virtual BaseType BaseTyp { get; set; }
        public virtual List<BaseType> GenericArguments { get; set; }
        [DataMember] public virtual Tuple<BaseAccessLevel, BaseSealedEnum, BaseAbstractEnum, BaseStaticEnum> Modifiers { get; set; }
        [DataMember] public virtual BaseTypeKind TypeKind { get; set; }
        public virtual List<BaseType> ImplementedInterfaces { get; set; }
        public virtual List<BaseType> NestedTypes { get; set; }
        public virtual List<BaseProperty> Properties { get; set; }
        public virtual BaseType DeclaringType { get; set; }
        public virtual List<BaseMethod> Methods { get; set; }
        public virtual List<BaseMethod> Constructors { get; set; }
        public virtual List<BaseParameter> Attributes { get; set; }
    }
}
