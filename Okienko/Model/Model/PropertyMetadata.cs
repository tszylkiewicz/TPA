﻿using Model.Logger;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model
{
    [DataContract(IsReference = true)]
    public class PropertyMetadata
    {
        LogWriter logWriter;
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public TypeMetadata PropertyType { get; set; }

        public PropertyMetadata(string name, TypeMetadata propertyType)
        {
            this.Name = name;
            this.PropertyType = propertyType;
            logWriter = new LogWriter("Utworzono obiekt klasy PropertyMetadata: " + this.Name);
        }

        public static List<PropertyMetadata> EmitProperties(Type type)
        {
            List<PropertyInfo> props = type
                .GetProperties(BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Public |
                               BindingFlags.Static | BindingFlags.Instance).ToList();//

            return props.Where(t => t.GetGetMethod().GetVisible() || t.GetSetMethod().GetVisible())
                .Select(t => new PropertyMetadata(t.Name, TypeMetadata.EmitReference(t.PropertyType))).ToList();
        }
        
    }
}