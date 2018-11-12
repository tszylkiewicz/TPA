using Model.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class TreeViewType : TreeViewItem
    {
        public List<PropertyMetadata> Properties { get; set; }
        public TreeViewType(TypeMetadata type) : base(type.Name)
        {
            this.Properties = type.Properties;
        }

        public override void Build(ObservableCollection<TreeViewItem> children)
        {
            if (Properties != null)
            {
                foreach (PropertyMetadata property in Properties)
                {
                    children.Add(new TreeViewProperty(property));
                }
            }
        }
    }
}
