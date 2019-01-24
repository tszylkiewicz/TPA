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
    public class TreeViewMethod : TreeViewItem
    {
        public MethodMetadata Method { get; set; }
        public TreeViewMethod(MethodMetadata method) : base(GetFullName(method))
        {
            this.Method = method;
        }
        public override void Build(ObservableCollection<TreeViewItem> children)
        {
            if (Method.GenericArguments != null)
            {
                foreach (TypeMetadata genericArgument in Method.GenericArguments)
                {
                    children.Add(new TreeViewType(genericArgument));
                }
            }

            if (Method.Parameters != null)
            {
                foreach (ParameterMetadata parameter in Method.Parameters)
                {
                    children.Add(new TreeViewParameter(parameter));
                }
            }

            if (Method.ReturnType != null)
            {
                children.Add(new TreeViewType(Method.ReturnType));
            }
        }
        public static string GetFullName(MethodMetadata model)
        {
            return model.GetFullName();
        }
    }
}
