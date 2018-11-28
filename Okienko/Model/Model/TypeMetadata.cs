using Model.Logger;
using Model.Singleton;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Model.Model
{
    public class TypeMetadata
    {
        #region Properties
        [Key]
        public int idType { get; set; }
        public string Name;
        public string NamespaceName;

        [ForeignKey("NamespaceMetadata")]
        public int idNamespace { get; set; }
        public NamespaceMetadata NamespaceMetadata { get; set; }

        public TypeMetadata BaseType;
        public List<TypeMetadata> GenericArguments;
        public Tuple<AccessLevel, SealedEnum, AbstractEnum> Modifiers;
        public TypeKind TypeKind;
        public List<Attribute> Attributes;
        public List<TypeMetadata> ImplementedInterfaces;
        public List<TypeMetadata> NestedTypes;
        public List<PropertyMetadata> Properties;
        public TypeMetadata DeclaringType;
        public List<MethodMetadata> Methods;
        public List<MethodMetadata> Constructors;
        [NotMapped]
        private LogWriter logWriter;
        #endregion

        #region Constructors
        public TypeMetadata(Type type)
        {
            this.Name = type.Name;
            if (!SingletonDictionary.Instance.ContainsKey(Name))
            {
                SingletonDictionary.Instance.Add(Name, this);
                logWriter = new LogWriter("Utworzono obiekt klasy TypeMetadata: " + Name);
            }
            else
            {
                logWriter = new LogWriter("Odwołano się do obiektu klasy TypeMetadata: " + Name);
            }
            this.DeclaringType = EmitDeclaringType(type.DeclaringType);
            this.Constructors = MethodMetadata.EmitMethods(type.GetConstructors()).ToList();
            this.Methods = MethodMetadata.EmitMethods(type.GetMethods()).ToList();
            this.NestedTypes = EmitNestedTypes(type.GetNestedTypes()).ToList();
            this.ImplementedInterfaces = EmitImplements(type.GetInterfaces()).ToList();
            this.GenericArguments = !type.IsGenericTypeDefinition ? null : TypeMetadata.EmitGenericArguments(type.GetGenericArguments()).ToList();
            this.Modifiers = EmitModifiers(type);
            this.BaseType = EmitExtends(type.BaseType);
            this.Properties = PropertyMetadata.EmitProperties(type.GetProperties()).ToList();
            this.TypeKind = GetTypeKind(type);
            this.Attributes = type.GetCustomAttributes(false).Cast<Attribute>().ToList();
        }
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
        public static TypeMetadata EmitReference(Type type)
        {
            if (!type.IsGenericType)
                return new TypeMetadata(type.Name, type.GetNamespace());
            else
                return new TypeMetadata(type.Name, type.GetNamespace(), EmitGenericArguments(type.GetGenericArguments()));
        }
        public static IEnumerable<TypeMetadata> EmitGenericArguments(IEnumerable<Type> arguments)
        {
            return from Type _argument in arguments select EmitReference(_argument);
        }       
        public static void StoreType(Type type)
        {
            if (!SingletonDictionary.Instance.ContainsKey(type.Name))
            {
                new TypeMetadata(type);
            }
        }
        public TypeMetadata EmitDeclaringType(Type declaringType)
        {
            if (declaringType == null)
                return null;
            StoreType(declaringType);
            return EmitReference(declaringType);
        }
        public IEnumerable<TypeMetadata> EmitNestedTypes(IEnumerable<Type> nestedTypes)
        {
            return from _type in nestedTypes
                   where _type.GetVisible()
                   select new TypeMetadata(_type);
        }
        public IEnumerable<TypeMetadata> EmitImplements(IEnumerable<Type> interfaces)
        {
            return from currentInterface in interfaces
                   select EmitReference(currentInterface);
        }
        public static TypeKind GetTypeKind(Type type)
        {
            return type.IsEnum ? TypeKind.Enum :
                   type.IsValueType ? TypeKind.Struct :
                   type.IsInterface ? TypeKind.Interface :
                   TypeKind.Class;
        }
        static Tuple<AccessLevel, SealedEnum, AbstractEnum> EmitModifiers(Type type)
        {
            //set defaults 
            AccessLevel _access = AccessLevel.Private;
            AbstractEnum _abstract = AbstractEnum.NotAbstract;
            SealedEnum _sealed = SealedEnum.NotSealed;
            // check if not default 
            if (type.IsPublic)
                _access = AccessLevel.Public;
            else if (type.IsNestedPublic)
                _access = AccessLevel.Public;
            else if (type.IsNestedFamily)
                _access = AccessLevel.Protected;
            else if (type.IsNestedFamANDAssem)
                _access = AccessLevel.Internal;
            if (type.IsSealed)
                _sealed = SealedEnum.Sealed;
            if (type.IsAbstract)
                _abstract = AbstractEnum.Abstract;
            return new Tuple<AccessLevel, SealedEnum, AbstractEnum>(_access, _sealed, _abstract);
        }
        public static TypeMetadata EmitExtends(Type baseType)
        {
            if (baseType == null || baseType == typeof(Object) || baseType == typeof(ValueType) || baseType == typeof(Enum))
                return null;
            return EmitReference(baseType);
        }
        #endregion
    }
}
