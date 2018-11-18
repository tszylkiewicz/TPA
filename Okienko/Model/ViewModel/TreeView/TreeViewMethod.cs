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
        public TreeViewMethod(MethodMetadata method) : base(method.Name)
        {
            this.Method = method;
        }
        public override void Build(ObservableCollection<TreeViewItem> children)
        {
            if (Method.GenericArguments != null)
            {
                foreach (TypeMetadata argument in Method.GenericArguments)
                {
                    children.Add(new TreeViewType(SingletonDictionary.Instance.Get(argument.Name)));
                }
            }
            if (Method.Parameters != null)
            {
                foreach(ParameterMetadata parameter in Method.Parameters)
                {
                    children.Add(new TreeViewParameter(parameter));
                }
            }
            if (Method.ReturnType != null)
            {
                Console.WriteLine(Method.ReturnType.Name);
                //children.Add(new TreeViewType(SingletonDictionary.Instance.Get(Method.ReturnType.Name)));
            }
        }
    }
}
