using System;
using System.Collections.Generic;
using BaseModel.Enums;
using System.Runtime.Serialization;

namespace BaseModel
{
    public abstract class BaseMethod
    {
        public virtual string Name { get; set; }
        public virtual List<BaseType> GenericArguments { get; set; }
        public virtual MethodModifiers Modifiers { get; set; }
        public virtual BaseType ReturnType { get; set; }
        public virtual bool Extension { get; set; }
        public virtual List<BaseParameter> Parameters { get; set; }
    }
}
