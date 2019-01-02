using BaseModel;
using Model.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Model.Mappers
{
    public class MapperMethod
    {
        public BaseMethod MapDown(MethodMetadata model, Type methodModelType)
        {
            object methodModel = Activator.CreateInstance(methodModelType);
            PropertyInfo nameProperty = methodModelType.GetProperty("Name");
            PropertyInfo extensionProperty = methodModelType.GetProperty("Extension");
            PropertyInfo genericArgumentsProperty = methodModelType.GetProperty("GenericArguments",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            PropertyInfo modifiersProperty = methodModelType.GetProperty("Modifiers");
            PropertyInfo parametersProperty = methodModelType.GetProperty("Parameters",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            PropertyInfo returnTypeProperty = methodModelType.GetProperty("ReturnType",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);

            nameProperty?.SetValue(methodModel, model.Name);
            extensionProperty?.SetValue(methodModel, model.Extension);
            if (model.GenericArguments != null)
                genericArgumentsProperty?.SetValue(methodModel,
                    Converter.ConvertList(genericArgumentsProperty.PropertyType.GetGenericArguments()[0],
                        model.GenericArguments.Select(t => MapperType.EmitBaseType(t, genericArgumentsProperty.PropertyType.GetGenericArguments()[0])).ToList()));
            modifiersProperty?.SetValue(methodModel, model.Modifiers);
            if (model.Parameters != null)
                parametersProperty?.SetValue(methodModel,
                    Converter.ConvertList(parametersProperty.PropertyType.GetGenericArguments()[0],
                        model.Parameters.Select(p => new MapperParameter().MapDown(p, parametersProperty.PropertyType.GetGenericArguments()[0])).ToList()));
            if (model.ReturnType != null)
                returnTypeProperty?.SetValue(methodModel,
                    returnTypeProperty.PropertyType.Cast(MapperType.EmitBaseType(model.ReturnType, returnTypeProperty.PropertyType)));

            return (BaseMethod)methodModel;
        }

        public MethodMetadata MapUp(BaseMethod model)
        {
            MethodMetadata methodModel = new MethodMetadata();
            methodModel.Name = model.Name;
            methodModel.Extension = model.Extension;
            Type type = model.GetType();
            PropertyInfo genericArgumentsProperty = type.GetProperty("GenericArguments",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            if (genericArgumentsProperty?.GetValue(model) != null)
            {
                List<BaseType> genericArguments =
                    (List<BaseType>)Converter.ConvertList(typeof(BaseType),
                        (IList)genericArgumentsProperty?.GetValue(model));
                methodModel.GenericArguments =
                    genericArguments.Select(g => MapperType.EmitType(g)).ToList();
            }


            PropertyInfo parametersProperty = type.GetProperty("Parameters",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            if (parametersProperty?.GetValue(model) != null)
            {
                List<BaseParameter> parameters =
                    (List<BaseParameter>)Converter.ConvertList(typeof(BaseParameter),
                        (IList)parametersProperty?.GetValue(model));

                methodModel.Parameters = parameters
                    .Select(p => new MapperParameter().MapUp(p)).ToList();
            }

            PropertyInfo returnTypeProperty = type.GetProperty("ReturnType",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            BaseType returnType = (BaseType)returnTypeProperty?.GetValue(model);
            if (returnType != null)
                methodModel.ReturnType = MapperType.EmitType(returnType);
            return methodModel;
        }       
    }
}
