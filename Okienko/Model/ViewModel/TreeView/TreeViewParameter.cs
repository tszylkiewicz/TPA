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
    public class TreeViewParameter : TreeViewItem
    {
        public ParameterMetadata Parameter { get; set; }

        public TreeViewParameter(ParameterMetadata parameter) : base(parameter.Name)
        {
            this.Parameter = parameter;
        }
        public override void Build(ObservableCollection<TreeViewItem> children)
        {
            if(Parameter.Type != null)
            {
                children.Add(new TreeViewType(Parameter.Type));
            }
        }
    }
}
