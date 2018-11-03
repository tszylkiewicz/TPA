using Model.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okienko.ViewModel
{
    public class TreeViewItem
    {
        public TreeViewItem(Reflector reflector)
        {
            Children = new ObservableCollection<TreeViewItem>() { null };
            NamespacesAssembly = new ObservableCollection<NamespaceMetadata>(reflector.m_AssemblyModel.Namespaces);
            this.m_WasBuilt = false;
        }
        public TreeViewItem(NamespaceMetadata namespc)
        {
            Children = new ObservableCollection<TreeViewItem>() { null };
            TypesAssembly = new ObservableCollection<TypeMetadata>(namespc.m_Types);
            this.m_WasBuilt = false;
        }
        public TreeViewItem(TypeMetadata type)
        {
            Children = new ObservableCollection<TreeViewItem>() { null };
            PropertiesAssembly = new ObservableCollection<PropertyMetadata>(type.Properties);
            this.m_WasBuilt = false;
        }
        public TreeViewItem(PropertyMetadata prop)
        {
            Children = new ObservableCollection<TreeViewItem>() { null };
            TypesAssembly = new ObservableCollection<TypeMetadata>() { prop.m_TypeMetadata };
            this.m_WasBuilt = false;
        }
        public TreeViewItem()
        {
            Children = new ObservableCollection<TreeViewItem>() { null };
            this.m_WasBuilt = false;
        }
        public string Name { get; set; }
        public ObservableCollection<TreeViewItem> Children { get; set; }
        public ObservableCollection<NamespaceMetadata> NamespacesAssembly { get; set; }
        public ObservableCollection<TypeMetadata> TypesAssembly { get; set; }
        public ObservableCollection<PropertyMetadata> PropertiesAssembly { get; set; }
        public bool IsExpanded
        {
            get { return m_IsExpanded; }
            set
            {
                m_IsExpanded = value;
                if (m_WasBuilt)
                    return;
                Children.Clear();
                BuildMyself();
                m_WasBuilt = true;
            }
        }

        private bool m_WasBuilt;
        private bool m_IsExpanded;
        private void BuildMyself()
        {
            if(NamespacesAssembly != null)
            {
                foreach(NamespaceMetadata namespc in NamespacesAssembly)
                {
                    this.Children.Add(new TreeViewItem(namespc) { Name = namespc.m_NamespaceName });
                }
            }

            if (TypesAssembly != null)
            {
                foreach (TypeMetadata type in TypesAssembly)
                {
                    this.Children.Add(new TreeViewItem(type) { Name = type.m_typeName });
                }
            }

            if (PropertiesAssembly != null)
            {
                foreach (PropertyMetadata type in PropertiesAssembly)
                {
                    this.Children.Add(new TreeViewItem(type) { Name = type.m_Name });
                }
            }
        }
    }
}
