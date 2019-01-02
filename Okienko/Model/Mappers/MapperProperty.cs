using BaseModel;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Model.Mappers
{
    public class MapperProperty
    {
        public BaseProperty MapDown(PropertyMetadata model, Type propertyModelType)
        {
            object propertyModel = Activator.CreateInstance(propertyModelType);
            PropertyInfo nameProperty = propertyModelType.GetProperty("Name");
            PropertyInfo typeProperty = propertyModelType.GetProperty("Type",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            nameProperty?.SetValue(propertyModel, model.Name);

            if (model.PropertyType != null)
                typeProperty?.SetValue(propertyModel,
                    typeProperty.PropertyType.Cast(MapperType.EmitBaseType(model.PropertyType, typeProperty.PropertyType)));

            return (BaseProperty)propertyModel;
        }

        public PropertyMetadata MapUp(BaseProperty model)
        {
            PropertyMetadata propertyModel = new PropertyMetadata();
            propertyModel.Name = model.Name;
            Type type = model.GetType();
            PropertyInfo typeProperty = type.GetProperty("Type",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            BaseType typeModel = (BaseType)typeProperty?.GetValue(model);

            if (typeModel != null)
                propertyModel.PropertyType = MapperType.EmitType(typeModel);

            return propertyModel;
        }    
    }
}
