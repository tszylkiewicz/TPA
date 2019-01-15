using Model.Model;
using Model.Singleton;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel.TreeView
{
    public class TreeViewType : TreeViewItem
    {
        public TypeMetadata Type { get; set; }
        public TreeViewType(TypeMetadata type) : base(GetFullName(type))
        {
            this.Type = type;
        }

        public override void Build(ObservableCollection<TreeViewItem> children)
        {
            if (Type.Properties != null)
            {
                foreach (PropertyMetadata property in Type.Properties)
                {
                    children.Add(new TreeViewProperty(property));
                }
            }
            if (Type.Methods != null)
            {
                foreach (MethodMetadata method in Type.Methods)
                {
                    children.Add(new TreeViewMethod(method));
                }
            }
            if (Type.Constructors != null)
            {
                foreach (MethodMetadata constructor in Type.Constructors)
                {
                    children.Add(new TreeViewMethod(constructor));
                }
            }
            if (Type.BaseType != null)
            {
                children.Add(new TreeViewType(SingletonDictionary.Instance.Get(Type.BaseType.Name)));
            }
            if (Type.DeclaringType != null)
            {
                children.Add(new TreeViewType(SingletonDictionary.Instance.Get(Type.DeclaringType.Name)));
            }
            if (Type.GenericArguments != null)
            {
                foreach (TypeMetadata typ in Type.GenericArguments)
                {
                    children.Add(new TreeViewType(SingletonDictionary.Instance.Get(typ.Name)));
                }
            }
            if (Type.ImplementedInterfaces != null)
            {
                foreach (TypeMetadata typ in Type.ImplementedInterfaces)
                {
                    children.Add(new TreeViewType(SingletonDictionary.Instance.Get(typ.Name)));
                }
            }
            if (Type.NestedTypes != null)
            {
                foreach (TypeMetadata typ in Type.NestedTypes)
                {
                    children.Add(new TreeViewType(SingletonDictionary.Instance.Get(typ.Name)));
                }
            }
            if (Type.Attributes != null)
            {
                foreach (ParameterMetadata parameter in Type.Attributes)
                {
                    children.Add(new TreeViewParameter(parameter));
                }
            }
        }
        public static string GetFullName(TypeMetadata model)
        {
            return model.GetFullName();
        }
    }
}
