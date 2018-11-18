using Model.ViewModel;
using Model.ViewModel.TreeView;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WPF
{
    [ValueConversion(typeof(TreeViewItem), typeof(string))]
    class Converter : IValueConverter
    {
        public static Converter Instance = new Converter();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Type type = value.GetType();
            string typeString = type == typeof(TreeViewAssembly) ? "Assembly":
                type == typeof(TreeViewNamespace) ? "Namespace" :
                type == typeof(TreeViewProperty) ? "Property" :
                type == typeof(TreeViewMethod) ? "Method" :
                type == typeof(TreeViewParameter) ? "Parameter" :
                type == typeof(TreeViewType) ? "Type" : "";
            return '[' + typeString + ']';
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
