﻿using BaseModel;
using BaseModel.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBaseModel
{
    [Table("TypeMetadata")]
    public class DataBaseType : BaseType
    {
        [Key]
        public override string Name { get; set; }
        public override string NamespaceName { get; set; }
        public int? NamespaceId { get; set; }
        public new DataBaseType BaseTyp { get; set; }
        public new List<DataBaseType> GenericArguments { get; set; }
        public override TypeModifiers Modifiers { get; set; }
        public override TypeKind TypeKind { get; set; }
        public new List<DataBaseType> ImplementedInterfaces { get; set; }
        public new List<DataBaseType> NestedTypes { get; set; }
        public new List<DataBaseProperty> Properties { get; set; }
        public new DataBaseType DeclaringType { get; set; }
        public new List<DataBaseMethod> Methods { get; set; }
        public new List<DataBaseMethod> Constructors { get; set; }
        public new List<DataBaseParameter> Attributes { get; set; }


        [InverseProperty("BaseTyp")]
        public virtual ICollection<DataBaseType> TypeBaseTypes { get; set; }
        [InverseProperty("DeclaringType")]
        public virtual ICollection<DataBaseType> TypeDeclaringTypes { get; set; }
        public virtual ICollection<DataBaseMethod> MethodGenericArguments { get; set; }
        public virtual ICollection<DataBaseType> TypeGenericArguments { get; set; }
        public virtual ICollection<DataBaseType> TypeImplementedInterfaces { get; set; }
        public virtual ICollection<DataBaseType> TypeNestedTypes { get; set; }

        public DataBaseType()
        {
            MethodGenericArguments = new HashSet<DataBaseMethod>();
            TypeGenericArguments = new HashSet<DataBaseType>();
            TypeImplementedInterfaces = new HashSet<DataBaseType>();
            TypeNestedTypes = new HashSet<DataBaseType>();
            Constructors = new List<DataBaseMethod>();
            Attributes = new List<DataBaseParameter>();
            GenericArguments = new List<DataBaseType>();
            ImplementedInterfaces = new List<DataBaseType>();
            Methods = new List<DataBaseMethod>();
            NestedTypes = new List<DataBaseType>();
            Properties = new List<DataBaseProperty>();
        }
    }
}
