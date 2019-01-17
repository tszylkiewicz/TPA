using BaseModel;
using BaseModel.Enums;
using System;
using System.Collections.Generic;

namespace DataBaseModel
{
    public class DataBaseMethod : BaseMethod
    {
        public override string Name { get; set; }
        public new List<DataBaseType> GenericArguments { get; set; }
        public override Tuple<BaseAccessLevel, BaseAbstractEnum, BaseStaticEnum, BaseVirtualEnum> Modifiers { get; set; }
        public new DataBaseType ReturnType { get; set; }
        public override bool Extension { get; set; }
        public new List<DataBaseParameter> Parameters { get; set; }
        public int Id { get; set; }

        public virtual ICollection<DataBaseType> TypeConstructors { get; set; }
        public virtual ICollection<DataBaseType> TypeMethods { get; set; }


        public DataBaseMethod()
        {
            GenericArguments = new List<DataBaseType>();
            Parameters = new List<DataBaseParameter>();
            TypeConstructors = new HashSet<DataBaseType>();
            TypeMethods = new HashSet<DataBaseType>();
        }
    }
}
