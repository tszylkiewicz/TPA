using Model.Model;
using Model.Singleton;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.ViewModel
{
    public class TreeViewItem
    {
        #region Properties
        public string Name { get; set; }
        public ObservableCollection<TreeViewItem> Children { get; set; }
        private bool m_WasBuilt;
        private bool m_IsExpanded;
        public ObservableCollection<NamespaceMetadata> NamespacesAssembly { get; set; }
        public ObservableCollection<TypeMetadata> TypesAssembly { get; set; }
        public List<PropertyMetadata> PropertiesAssembly { get; set; }
        #endregion
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
            PropertiesAssembly = type.Properties;
            this.m_WasBuilt = false;
        }
        public TreeViewItem(PropertyMetadata prop)
        {
            Children = new ObservableCollection<TreeViewItem>() { null };
            TypesAssembly = new ObservableCollection<TypeMetadata>() { prop.m_TypeMetadata };
            this.m_WasBuilt = false;
        }
        public TreeViewItem(string name)
        {
            this.Name = name;
            Children = new ObservableCollection<TreeViewItem>() { null };
            this.m_WasBuilt = false;
        }
        
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

        private void BuildMyself()
        {
            if (NamespacesAssembly != null)
            {
                foreach (NamespaceMetadata namespc in NamespacesAssembly)
                {
                    this.Children.Add(new TreeViewItem(namespc) { Name = namespc.m_NamespaceName });
                }
            }

            if (TypesAssembly != null)
            {
                foreach (TypeMetadata type in TypesAssembly)
                {
                    this.Children.Add(new TreeViewItem(SingletonDictionary.Instance.Get(type.m_typeName)) { Name = type.m_typeName });
                }
            }

            if (PropertiesAssembly != null)
            {
                foreach (PropertyMetadata type in PropertiesAssembly)
                {
                    this.Children.Add(new TreeViewItem(SingletonDictionary.Instance.Get(type.m_TypeMetadata.m_typeName)) { Name = type.m_Name });
                }
            }
        }
    }
}
