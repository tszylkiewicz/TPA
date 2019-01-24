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
    public class TreeViewProperty : TreeViewItem
    {
        public PropertyMetadata Property { get; set; }
        public TreeViewProperty(PropertyMetadata property) : base(property.Name)
        {
            this.Property = property;
        }

        public override void Build(ObservableCollection<TreeViewItem> children)
        {
            if (Property.PropertyType != null)
            {
                children.Add(new TreeViewType(Property.PropertyType));
            }
        }
    }
}
