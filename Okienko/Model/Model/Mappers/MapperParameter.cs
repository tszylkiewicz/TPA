using BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model.Mappers
{
    public class MapperParameter
    {
        public BaseParameter MapDown(ParameterMetadata model, Type parameterModelType)
        {
            object parameterModel = Activator.CreateInstance(parameterModelType);
            PropertyInfo nameProperty = parameterModelType.GetProperty("Name");
            PropertyInfo typeProperty = parameterModelType.GetProperty("Type",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            nameProperty?.SetValue(parameterModel, model.Name);
            if (model.Type != null)
                typeProperty?.SetValue(parameterModel,
                    typeProperty.PropertyType.Cast(MapperType.EmitBaseType(model.Type, typeProperty.PropertyType)));

            return (BaseParameter)parameterModel;
        }

        public ParameterMetadata MapUp(BaseParameter model)
        {
            ParameterMetadata parameterModel = new ParameterMetadata();
            parameterModel.Name = model.Name;
            Type type = model.GetType();
            PropertyInfo typeProperty = type.GetProperty("Type",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            BaseType typeModel = (BaseType)typeProperty?.GetValue(model);
            if (typeModel != null)
                parameterModel.Type = MapperType.EmitType(typeModel);
            return parameterModel;
        }
    }
}
