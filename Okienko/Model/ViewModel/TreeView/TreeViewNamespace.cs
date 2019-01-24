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
    public class TreeViewNamespace : TreeViewItem
    {
        public List<TypeMetadata> Types { get; set; }

        public TreeViewNamespace(NamespaceMetadata namespaceMetadata) : base(namespaceMetadata.Name)
        {
            this.Types = namespaceMetadata.Types;
        }

        public override void Build(ObservableCollection<TreeViewItem> children)
        {
            if (Types != null)
            {
                foreach (TypeMetadata type in Types)
                {
                    children.Add(new TreeViewType(type));
                }
            }
        }
    }
}
