using Model.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model
{
    public class PropertyMetadata
    {
        LogWriter logWriter;
        public string m_Name;
        public TypeMetadata m_TypeMetadata;

        internal static IEnumerable<PropertyMetadata> EmitProperties(IEnumerable<PropertyInfo> props)
        {
            return from prop in props
                   where prop.GetGetMethod().GetVisible() || prop.GetSetMethod().GetVisible()
                   select new PropertyMetadata(prop.Name, TypeMetadata.EmitReference(prop.PropertyType));
        }

        private PropertyMetadata(string propertyName, TypeMetadata propertyType)
        {
            m_Name = propertyName;
            m_TypeMetadata = propertyType;
            logWriter = new LogWriter("Utworzono obiekt klasy PropertyMetadata: " + m_Name);
        }
    }
}