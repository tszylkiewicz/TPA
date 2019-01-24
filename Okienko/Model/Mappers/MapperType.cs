using BaseModel;
using BaseModel.Enums;
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
    public class MapperType
    {
        public static Dictionary<string, BaseType> BaseTypes = new Dictionary<string, BaseType>();
        public static Dictionary<string, TypeMetadata> Types = new Dictionary<string, TypeMetadata>();

        public static BaseType EmitBaseType(TypeMetadata model, Type type)
        {
            return new MapperType().MapDown(model, type);
        }

        public static TypeMetadata EmitType(BaseType model)
        {
            return new MapperType().MapUp(model);
        }

        private void FillBaseType(TypeMetadata model, BaseType typModel)
        {
            Type typeModelType = typModel.GetType();

            typeModelType.GetProperty("Name")?.SetValue(typModel, model.Name);
            typeModelType.GetProperty("TypeKind")?.SetValue(typModel, model.TypeKind);
            typeModelType.GetProperty("NamespaceName")?.SetValue(typModel, model.NamespaceName);
            typeModelType.GetProperty("Modifiers")?.SetValue(typModel, model.Modifiers ?? new TypeModifiers());

            if (model.BaseTyp != null)
            {
                typeModelType.GetProperty("BaseTyp",
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                    ?.SetValue(typModel, typeModelType.Cast(EmitBaseType(model.BaseTyp, typeModelType)));
            }

            if (model.DeclaringType != null)
            {
                typeModelType.GetProperty("DeclaringType",
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                    ?.SetValue(typModel, typeModelType.Cast(EmitBaseType(model.DeclaringType, typeModelType)));
            }

            if (model.NestedTypes != null)
            {
                PropertyInfo nestedTypesProperty = typeModelType.GetProperty("NestedTypes",
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
                nestedTypesProperty?.SetValue(typModel,
                    Converter.ConvertList(typeModelType,
                        model.NestedTypes?.Select(c => EmitBaseType(c, typeModelType)).ToList()));
            }

            if (model.GenericArguments != null)
            {
                PropertyInfo genericArgumentsProperty = typeModelType.GetProperty("GenericArguments",
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
                genericArgumentsProperty?.SetValue(typModel,
                    Converter.ConvertList(typeModelType,
                        model.GenericArguments?.Select(c => EmitBaseType(c, typeModelType)).ToList()));
            }

            if (model.ImplementedInterfaces != null)
            {
                PropertyInfo implementedInterfacesProperty = typeModelType.GetProperty("ImplementedInterfaces",
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
                implementedInterfacesProperty?.SetValue(typModel,
                    Converter.ConvertList(typeModelType,
                        model.ImplementedInterfaces?.Select(c => EmitBaseType(c, typeModelType)).ToList()));
            }

            if (model.Attributes != null)
            {
                PropertyInfo fieldsProperty = typeModelType.GetProperty("Attributes",
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
                fieldsProperty?.SetValue(typModel,
                    Converter.ConvertList(fieldsProperty.PropertyType.GetGenericArguments()[0],
                        model.Attributes?.Select(c =>
                            new MapperParameter().MapDown(c,
                                fieldsProperty?.PropertyType.GetGenericArguments()[0])).ToList()));
            }

            if (model.Methods != null)
            {
                PropertyInfo methodsProperty = typeModelType.GetProperty("Methods",
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
                methodsProperty?.SetValue(typModel,
                    Converter.ConvertList(methodsProperty.PropertyType.GetGenericArguments()[0],
                        model.Methods?.Select(m =>
                                new MapperMethod().MapDown(m,
                                    methodsProperty?.PropertyType.GetGenericArguments()[0]))
                            .ToList()));
            }

            if (model.Constructors != null)
            {
                PropertyInfo constructorsProperty = typeModelType.GetProperty("Constructors",
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
                constructorsProperty?.SetValue(typModel,
                    Converter.ConvertList(constructorsProperty.PropertyType.GetGenericArguments()[0],
                        model.Constructors?.Select(c =>
                            new MapperMethod().MapDown(c,
                                constructorsProperty?.PropertyType.GetGenericArguments()[0])).ToList()));
            }

            if (model.Properties != null)
            {
                PropertyInfo propertiesProperty = typeModelType.GetProperty("Properties",
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
                propertiesProperty?.SetValue(typModel,
                    Converter.ConvertList(propertiesProperty.PropertyType.GetGenericArguments()[0],
                        model.Properties?.Select(c =>
                            new MapperProperty().MapDown(c,
                                propertiesProperty?.PropertyType.GetGenericArguments()[0])).ToList()));
            }
        }

        private void FillType(BaseType model, TypeMetadata typeModel)
        {
            typeModel.Name = model.Name;
            typeModel.TypeKind = model.TypeKind;
            typeModel.NamespaceName = model.NamespaceName;
            typeModel.Modifiers = model.Modifiers ?? new TypeModifiers();

            Type type = model.GetType();
            PropertyInfo baseTypeProperty = type.GetProperty("BaseTyp",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            BaseType baseType = (BaseType)baseTypeProperty?.GetValue(model);
            typeModel.BaseTyp = EmitType(baseType);

            PropertyInfo declaringTypeProperty = type.GetProperty("DeclaringType",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            BaseType declaringType = (BaseType)declaringTypeProperty?.GetValue(model);
            typeModel.DeclaringType = EmitType(declaringType);

            PropertyInfo nestedTypesProperty = type.GetProperty("NestedTypes",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            if (nestedTypesProperty?.GetValue(model) != null)
            {
                List<BaseType> nestedTypes = (List<BaseType>)Converter.ConvertList(typeof(BaseType),
                    (IList)nestedTypesProperty?.GetValue(model));
                typeModel.NestedTypes = nestedTypes?.Select(n => EmitType(n)).ToList();
            }

            PropertyInfo genericArgumentsProperty = type.GetProperty("GenericArguments",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            if (genericArgumentsProperty?.GetValue(model) != null)
            {
                List<BaseType> genericArguments =
                    (List<BaseType>)Converter.ConvertList(typeof(BaseType),
                        (IList)genericArgumentsProperty?.GetValue(model));
                typeModel.GenericArguments = genericArguments?.Select(g => EmitType(g)).ToList();
            }

            PropertyInfo implementedInterfacesProperty = type.GetProperty("ImplementedInterfaces",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            if (implementedInterfacesProperty?.GetValue(model) != null)
            {
                List<BaseType> implementedInterfaces =
                    (List<BaseType>)Converter.ConvertList(typeof(BaseType),
                        (IList)implementedInterfacesProperty?.GetValue(model));
                typeModel.ImplementedInterfaces =
                    implementedInterfaces?.Select(i => EmitType(i)).ToList();
            }

            PropertyInfo fieldsProperty = type.GetProperty("Attributes",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            if (fieldsProperty?.GetValue(model) != null)
            {
                List<BaseParameter> fields =
                    (List<BaseParameter>)Converter.ConvertList(typeof(BaseParameter),
                        (IList)fieldsProperty?.GetValue(model));
                typeModel.Attributes = fields?.Select(g => new MapperParameter().MapUp(g))
                    .ToList();
            }

            PropertyInfo methodsProperty = type.GetProperty("Methods",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            if (methodsProperty?.GetValue(model) != null)
            {
                List<BaseMethod> methods = (List<BaseMethod>)Converter.ConvertList(typeof(BaseMethod),
                    (IList)methodsProperty?.GetValue(model));
                typeModel.Methods = methods?.Select(c => new MapperMethod().MapUp(c)).ToList();
            }

            PropertyInfo constructorsProperty = type.GetProperty("Constructors",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            if (constructorsProperty?.GetValue(model) != null)
            {
                List<BaseMethod> constructors =
                    (List<BaseMethod>)Converter.ConvertList(typeof(BaseMethod),
                        (IList)constructorsProperty?.GetValue(model));
                typeModel.Constructors = constructors?.Select(c => new MapperMethod().MapUp(c))
                    .ToList();
            }

            PropertyInfo propertiesProperty = type.GetProperty("Properties",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            if (propertiesProperty?.GetValue(model) != null)
            {
                List<BaseProperty> properties =
                    (List<BaseProperty>)Converter.ConvertList(typeof(BaseProperty),
                        (IList)propertiesProperty?.GetValue(model));
                typeModel.Properties = properties?.Select(g => new MapperProperty().MapUp(g))
                    .ToList();
            }
        }

        public TypeMetadata MapUp(BaseType model)
        {
            TypeMetadata typeModel = new TypeMetadata();
            if (model == null)
                return null;

            if (!Types.ContainsKey(model.Name))
            {
                Types.Add(model.Name, typeModel);
                FillType(model, typeModel);
            }
            return Types[model.Name];

        }

        public BaseType MapDown(TypeMetadata model, Type typeModelType)
        {

            object typeModel = Activator.CreateInstance(typeModelType);
            if (model == null)
                return null;
            if (!BaseTypes.ContainsKey(model.Name))
            {               
                BaseTypes.Add(model.Name, (BaseType)typeModel);
                FillBaseType(model, (BaseType)typeModel);
            }
            return BaseTypes[model.Name];
        }
    }
}
