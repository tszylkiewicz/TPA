using Model.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class TreeViewAssembly : TreeViewItem
    {
        public List<NamespaceMetadata> Namespaces { get; set; }
        public TreeViewAssembly(AssemblyMetadata assembly) : base(assembly.Name)
        {
            Namespaces = assembly.Namespaces;
        }

        public override void Build(ObservableCollection<TreeViewItem> children)
        {
            if (Namespaces != null)
            {
                foreach (NamespaceMetadata namespaceMetadata in Namespaces)
                {
                    children.Add(new TreeViewNamespace(namespaceMetadata));
                }
            }
        }
    }
}
