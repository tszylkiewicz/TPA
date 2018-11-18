using Model.Model;
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
        public TreeViewType(TypeMetadata type) : base(type.Name)
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
        }
    }
}
