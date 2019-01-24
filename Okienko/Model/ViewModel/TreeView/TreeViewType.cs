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
            if (Type.BaseTyp != null)
            {
                children.Add(new TreeViewType(Type.BaseTyp));
            }
            if (Type.DeclaringType != null)
            {
                children.Add(new TreeViewType(Type.DeclaringType));
            }
            if (Type.Properties != null)
            {
                foreach (PropertyMetadata PropertyMetadata in Type.Properties)
                {
                    children.Add(new TreeViewProperty(PropertyMetadata));
                }
            }
            if (Type.Attributes != null)
            {
                foreach (ParameterMetadata ParameterMetadata in Type.Attributes)
                {
                    children.Add(new TreeViewParameter(ParameterMetadata));
                }
            }
            if (Type.GenericArguments != null)
            {
                foreach (TypeMetadata TypeMetadata in Type.GenericArguments)
                {
                    children.Add(new TreeViewType(TypeMetadata));
                }
            }
            if (Type.ImplementedInterfaces != null)
            {
                foreach (TypeMetadata TypeMetadata in Type.ImplementedInterfaces)
                {
                    children.Add(new TreeViewType(TypeMetadata));
                }
            }
            if (Type.NestedTypes != null)
            {
                foreach (TypeMetadata TypeMetadata in Type.NestedTypes)
                {
                    children.Add(new TreeViewType(TypeMetadata));
                }
            }
            if (Type.Methods != null)
            {
                foreach (MethodMetadata MethodMetadata in Type.Methods)
                {
                    children.Add(new TreeViewMethod(MethodMetadata));
                }
            }
            if (Type.Constructors != null)
            {
                foreach (MethodMetadata MethodMetadata in Type.Constructors)
                {
                    children.Add(new TreeViewMethod(MethodMetadata));
                }
            }
        }

        public static string GetFullName(TypeMetadata model)
        {
            return model.GetFullName();
        }
    }
}
