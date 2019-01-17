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
    public class MapperNamespace
    {
        public BaseNamespace MapDown(NamespaceMetadata model, Type namespaceModelType)
        {
            object namespaceModel = Activator.CreateInstance(namespaceModelType);
            PropertyInfo nameProperty = namespaceModelType.GetProperty("Name");
            PropertyInfo namespaceModelsProperty = namespaceModelType.GetProperty("Types",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            nameProperty?.SetValue(namespaceModel, model.Name);            
            namespaceModelsProperty?.SetValue(namespaceModel,
                Converter.ConvertList(namespaceModelsProperty.PropertyType.GetGenericArguments()[0],
                    model.Types.Select(t => new MapperType().MapDown(t, namespaceModelsProperty.PropertyType.GetGenericArguments()[0])).ToList()));
            return (BaseNamespace)namespaceModel;
        }

        public NamespaceMetadata MapUp(BaseNamespace model)
        {
            NamespaceMetadata namespaceModel = new NamespaceMetadata();
            namespaceModel.Name = model.Name;
            Type type = model.GetType();           
            PropertyInfo typesProperty = type.GetProperty("Types",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            List<BaseType> types = (List<BaseType>)Converter.ConvertList(typeof(BaseType), (IList)typesProperty?.GetValue(model));
            if (types != null)
                namespaceModel.Types = types.Select(n => MapperType.EmitType(n)).ToList();
            return namespaceModel;
        }
    }
}
