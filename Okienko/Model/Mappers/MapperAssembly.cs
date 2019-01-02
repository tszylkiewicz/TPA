using BaseModel;
using Model.Mappers;
using Model.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Model.Mappers
{
    public class MapperAssembly
    {     
        public static BaseAssembly MapDown(AssemblyMetadata model, Type assemblyModelType)
        {
            object assemblyModel = Activator.CreateInstance(assemblyModelType);
            PropertyInfo nameProperty = assemblyModelType.GetProperty("Name");
            PropertyInfo namespaceModelsProperty = assemblyModelType.GetProperty("NamespaceMetadatas",
                BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
            nameProperty?.SetValue(assemblyModel, model.Name);
            namespaceModelsProperty?.SetValue(
                assemblyModel,
                Converter.ConvertList(namespaceModelsProperty.PropertyType.GetGenericArguments()[0],
                    model.Namespaces.Select(n => new MapperNamespace().MapDown(n, namespaceModelsProperty.PropertyType.GetGenericArguments()[0])).ToList()));
            return (BaseAssembly)assemblyModel;
        }

        public static AssemblyMetadata MapUp(BaseAssembly assembly)
        {
            AssemblyMetadata assemblyModel = new AssemblyMetadata();
            Type type = assembly.GetType();
            assemblyModel.Name = assembly.Name;
            PropertyInfo namespaceModelsProperty = type.GetProperty("NamespaceMetadatas",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            List<BaseNamespace> namespaceModels = (List<BaseNamespace>)Converter.ConvertList(typeof(BaseNamespace), (IList)namespaceModelsProperty?.GetValue(assembly));
            if (namespaceModels != null)
                assemblyModel.Namespaces = namespaceModels.Select(n => new MapperNamespace().MapUp(n)).ToList();
            return assemblyModel;
        }
    }
}
