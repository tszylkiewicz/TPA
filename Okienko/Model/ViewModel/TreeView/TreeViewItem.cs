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
    public abstract class TreeViewItem
    {
        public string Name { get; set; }
        public ObservableCollection<TreeViewItem> Children { get; set; }
        private bool wasBuilt;
        private bool isExpanded;                  
       
        public TreeViewItem(string name)
        {
            this.Name = name;
            Children = new ObservableCollection<TreeViewItem>() { null };
            this.wasBuilt = false;
        }
        
        public bool IsExpanded
        {
            get { return isExpanded; }
            set
            {
                isExpanded = value;
                if (wasBuilt)
                    return;
                Children.Clear();
                Build(Children);
                wasBuilt = true;
            }
        }

        public abstract void Build(ObservableCollection<TreeViewItem> children);
    }
}
