using Okienko.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okienko
{
    public class TreeViewItem
    {
        public TreeViewItem()
        {
            Children = new ObservableCollection<TreeViewItem>() { null };
            this.m_WasBuilt = false;
        }
        public TreeViewItem(Reflector reflector)
        {
            Children = new ObservableCollection<TreeViewItem>() { null };
            ChildrenAssembly = new ObservableCollection<TypeMetadata>(reflector.m_AssemblyModel.Types);
            this.m_WasBuilt = false;
        }
        public string Name { get; set; }
        public ObservableCollection<TreeViewItem> Children { get; set; }
        public ObservableCollection<TypeMetadata> ChildrenAssembly { get; set; }
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
            foreach (TypeMetadata type in ChildrenAssembly)
                this.Children.Add(new TreeViewItem() { Name = type.m_typeName });
            //for (int i = 0; i < random.Next(7); i++)
                //this.Children.Add(new TreeViewItem() { Name = "sample" + i });
        }
    }
}
