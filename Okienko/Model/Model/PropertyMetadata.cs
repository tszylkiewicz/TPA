using Model.Logger;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model
{
    public class PropertyMetadata
    {
        [Key]
        public int idProperty { get; set; }
        [NotMapped]
        LogWriter logWriter;
        public string Name { get; set; }
        public TypeMetadata PropertyType { get; set; }

        public PropertyMetadata(string name, TypeMetadata propertyType)
        {
            this.Name = name;
            this.PropertyType = propertyType;
            logWriter = new LogWriter("Utworzono obiekt klasy PropertyMetadata: " + this.Name);
        }

        public static IEnumerable<PropertyMetadata> EmitProperties(IEnumerable<PropertyInfo> props)
        {
            return from prop in props
                   where prop.GetGetMethod().GetVisible() || prop.GetSetMethod().GetVisible()
                   select new PropertyMetadata(prop.Name, TypeMetadata.EmitReference(prop.PropertyType));
        }
        
    }
}