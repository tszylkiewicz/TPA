using Model.Logger;
using Model.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model
{
    public class TypeMetadata
    {
        LogWriter logWriter;
        public string m_typeName;
        public string m_NamespaceName;
        private TypeMetadata m_BaseType;
        private IEnumerable<TypeMetadata> m_GenericArguments;
        //private Tuple<AccessLevel, SealedEnum, AbstractENum> m_Modifiers;
        private TypeKind m_TypeKind;
        private IEnumerable<Attribute> m_Attributes;
        private IEnumerable<TypeMetadata> m_ImplementedInterfaces;
        private IEnumerable<TypeMetadata> m_NestedTypes;
        private List<PropertyMetadata> m_Properties;
        private TypeMetadata m_DeclaringType;

        internal TypeMetadata(Type type)
        {
            m_typeName = type.Name;
            if(!SingletonDictionary.Instance.ContainsKey(m_typeName))
            {
                SingletonDictionary.Instance.Add(m_typeName, this);
                logWriter = new LogWriter("Utworzono obiekt klasy TypeMetadata: " + m_typeName);
            }
            else
            {
                logWriter = new LogWriter("Odwołano się do obiektu klasy TypeMetadata: " + m_typeName);
            }
            m_NamespaceName = type.Namespace;
            m_DeclaringType = EmitDeclaringType(type.DeclaringType);
            //m_Constructors = MethodMetadata.EmitMethods(type.GetConstructors());
            //m_Methods = MethodMetadata.EmitMethods(type.GetMethods());
            m_NestedTypes = EmitNestedTypes(type.GetNestedTypes());
            m_ImplementedInterfaces = EmitImplements(type.GetInterfaces());
            m_GenericArguments = !type.IsGenericTypeDefinition ? null : TypeMetadata.EmitGenericArguments(type.GetGenericArguments());
            //m_Modifiers = EmitModifiers(type);
            m_BaseType = EmitExtends(type.BaseType);
            m_Properties = PropertyMetadata.EmitProperties(type.GetProperties()).ToList();
            m_TypeKind = GetTypeKind(type);
            m_Attributes = type.GetCustomAttributes(false).Cast<Attribute>();
        }
        private TypeMetadata(string typeName, string namespaceName)
        {
            m_typeName = typeName;
            m_NamespaceName = namespaceName;
        }
        private TypeMetadata(string typeName, string namespaceName, IEnumerable<TypeMetadata> genericArguments) : this(typeName, namespaceName)
        {
            m_GenericArguments = genericArguments;
        }

        internal enum TypeKind
        {
            EnumType, StructType, InterfaceType, ClassType
        }
        internal static TypeMetadata EmitReference(Type type)
        {
            if (!type.IsGenericType)
                return new TypeMetadata(type.Name, type.GetNamespace());
            else
                return new TypeMetadata(type.Name, type.GetNamespace(), EmitGenericArguments(type.GetGenericArguments()));
        }
        internal static IEnumerable<TypeMetadata> EmitGenericArguments(IEnumerable<Type> arguments)
        {
            return from Type _argument in arguments select EmitReference(_argument);
        }

        public List<PropertyMetadata> Properties { get => m_Properties; set => m_Properties = value; }

        //public IEnumerable<PropertyMetadata> Properties { get => m_Properties; set => m_Properties = value; }
        //private IEnumerable<MethodMetadata> m_Methods;
        //private IEnumerable<MethodMetadata> m_Constructors;

        public static void StoreType(Type type)
        {
            if (!SingletonDictionary.Instance.ContainsKey(type.Name))
            {
                new TypeMetadata(type);
            }
        }
        private TypeMetadata EmitDeclaringType(Type declaringType)
        {
            if (declaringType == null)
                return null;
            StoreType(declaringType);
            return EmitReference(declaringType);
        }
        private IEnumerable<TypeMetadata> EmitNestedTypes(IEnumerable<Type> nestedTypes)
        {
            return from _type in nestedTypes
                   where _type.GetVisible()
                   select new TypeMetadata(_type);
        }
        private IEnumerable<TypeMetadata> EmitImplements(IEnumerable<Type> interfaces)
        {
            return from currentInterface in interfaces
                   select EmitReference(currentInterface);
        }
        private static TypeKind GetTypeKind(Type type) //#80 TPA: Reflection - Invalid return value of GetTypeKind() 
        {
            return type.IsEnum ? TypeKind.EnumType :
                   type.IsValueType ? TypeKind.StructType :
                   type.IsInterface ? TypeKind.InterfaceType :
                   TypeKind.ClassType;
        }
        /*static Tuple<AccessLevel, SealedEnum, AbstractENum> EmitModifiers(Type type)
        {
            //set defaults 
            AccessLevel _access = AccessLevel.IsPrivate;
            AbstractENum _abstract = AbstractENum.NotAbstract;
            SealedEnum _sealed = SealedEnum.NotSealed;
            // check if not default 
            if (type.IsPublic)
                _access = AccessLevel.IsPublic;
            else if (type.IsNestedPublic)
                _access = AccessLevel.IsPublic;
            else if (type.IsNestedFamily)
                _access = AccessLevel.IsProtected;
            else if (type.IsNestedFamANDAssem)
                _access = AccessLevel.IsProtectedInternal;
            if (type.IsSealed)
                _sealed = SealedEnum.Sealed;
            if (type.IsAbstract)
                _abstract = AbstractENum.Abstract;
            return new Tuple<AccessLevel, SealedEnum, AbstractENum>(_access, _sealed, _abstract);
        }*/
        private static TypeMetadata EmitExtends(Type baseType)
        {
            if (baseType == null || baseType == typeof(Object) || baseType == typeof(ValueType) || baseType == typeof(Enum))
                return null;
            StoreType(baseType);
            return EmitReference(baseType);
        }
    }
}
