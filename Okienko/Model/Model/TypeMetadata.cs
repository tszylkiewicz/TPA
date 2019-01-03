
using Model.Singleton;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Model.Model
{
    public class TypeMetadata
    {
        #region Properties
        public string Name { get; set; }
        public string NamespaceName { get; set; }


        public TypeMetadata BaseType { get; set; }
        public List<TypeMetadata> GenericArguments { get; set; }
        public Tuple<AccessLevel, SealedEnum, AbstractEnum, StaticEnum> Modifiers { get; set; }
        public TypeKind TypeKind { get; set; }
        public List<TypeMetadata> ImplementedInterfaces { get; set; }
        public List<TypeMetadata> NestedTypes { get; set; }
        public List<PropertyMetadata> Properties { get; set; }
        public TypeMetadata DeclaringType { get; set; }
        public List<MethodMetadata> Methods { get; set; }
        public List<MethodMetadata> Constructors { get; set; }
        public List<ParameterMetadata> Attributes { get; set; }

        #endregion

        #region Constructors
        public TypeMetadata(Type type)
        {
            this.Name = type.Name;
            if (!SingletonDictionary.Instance.ContainsKey(Name))
            {
                SingletonDictionary.Instance.Add(Name, this);
            }
            this.DeclaringType = EmitDeclaringType(type.DeclaringType);
            this.Constructors = MethodMetadata.EmitConstructors(type);
            this.Methods = MethodMetadata.EmitMethods(type);
            this.NestedTypes = EmitNestedTypes(type);
            this.ImplementedInterfaces = EmitImplements(type.GetInterfaces()).ToList();
            this.GenericArguments = !type.IsGenericTypeDefinition ? null : EmitGenericArguments(type);
            this.Modifiers = EmitModifiers(type);
            this.BaseType = EmitExtends(type.BaseType);
            this.Properties = PropertyMetadata.EmitProperties(type);
            this.TypeKind = GetTypeKind(type);
            this.Attributes = EmitAttributes(type);
        }

        public TypeMetadata() { }

        public TypeMetadata(string typeName, string namespaceName)
        {
            this.Name = typeName;
            this.NamespaceName = namespaceName;
        }
        public TypeMetadata(string typeName, string namespaceName, IEnumerable<TypeMetadata> genericArguments) : this(typeName, namespaceName)
        {
            GenericArguments = genericArguments.ToList();
        }
        #endregion

        #region Methods                      
        public static void StoreType(Type type)
        {
            if (!SingletonDictionary.Instance.ContainsKey(type.Name))
            {
                new TypeMetadata(type);
            }
        }             
        public static TypeKind GetTypeKind(Type type)
        {
            return type.IsEnum ? TypeKind.Enum :
                   type.IsValueType ? TypeKind.Struct :
                   type.IsInterface ? TypeKind.Interface :
                   TypeKind.Class;
        }
        public static List<TypeMetadata> EmitGenericArguments(Type type)
        {
            List<Type> arguments = type.GetGenericArguments().ToList();
            foreach (Type typ in arguments)
            {
                StoreType(typ);
            }

            return arguments.Select(EmitReference).ToList();
        }
        private List<ParameterMetadata> EmitAttributes(Type type)
        {
            List<FieldInfo> fieldInfo = type.GetFields(BindingFlags.NonPublic | BindingFlags.DeclaredOnly |
                                                       BindingFlags.Public |
                                                       BindingFlags.Static | BindingFlags.Instance).ToList();

            List<ParameterMetadata> parameters = new List<ParameterMetadata>();
            foreach (FieldInfo field in fieldInfo)
            {
                StoreType(field.FieldType);
                parameters.Add(new ParameterMetadata(field.Name, EmitReference(field.FieldType)));
            }

            return parameters;
        }
        public static TypeMetadata EmitExtends(Type baseType)
        {
            if (baseType == null || baseType == typeof(Object) || baseType == typeof(ValueType) ||
                baseType == typeof(Enum))
                return null;
            StoreType(baseType);
            return EmitReference(baseType);
        }
        static Tuple<AccessLevel, SealedEnum, AbstractEnum, StaticEnum> EmitModifiers(Type type)
        {
            AccessLevel _access = type.IsPublic || type.IsNestedPublic ? AccessLevel.Public :
                type.IsNestedFamily ? AccessLevel.Protected :
                type.IsNestedFamANDAssem ? AccessLevel.Internal :
                AccessLevel.Private;
            StaticEnum _static = type.IsSealed && type.IsAbstract ? StaticEnum.Static : StaticEnum.NotStatic;
            SealedEnum _sealed = SealedEnum.NotSealed;
            AbstractEnum _abstract = AbstractEnum.NotAbstract;
            if (_static == StaticEnum.NotStatic)
            {
                _sealed = type.IsSealed ? SealedEnum.Sealed : SealedEnum.NotSealed;
                _abstract = type.IsAbstract ? AbstractEnum.Abstract : AbstractEnum.NotAbstract;
            }

            return new Tuple<AccessLevel, SealedEnum, AbstractEnum, StaticEnum>(_access, _sealed, _abstract, _static);
        }
        public static TypeMetadata EmitReference(Type type)
        {
            if (!type.IsGenericType)
                return new TypeMetadata(type.Name, type.GetNamespace());

            return new TypeMetadata(type.Name, type.GetNamespace(), EmitGenericArguments(type));
        }
        public TypeMetadata EmitDeclaringType(Type declaringType)
        {
            if (declaringType == null)
                return null;
            StoreType(declaringType);
            return EmitReference(declaringType);
        }
        public List<TypeMetadata> EmitNestedTypes(Type type)
        {
            List<Type> nestedTypes = type.GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic).ToList();
            foreach (Type typ in nestedTypes)
            {
                StoreType(typ);
            }

            return nestedTypes.Select(t => new TypeMetadata(t)).ToList();
        }
        public IEnumerable<TypeMetadata> EmitImplements(IEnumerable<Type> interfaces)
        {
            foreach (Type @interface in interfaces)
            {
                StoreType(@interface);
            }

            return from currentInterface in interfaces
                   select EmitReference(currentInterface);
        }
        #endregion
    }
}
