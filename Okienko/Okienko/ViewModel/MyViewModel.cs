using Microsoft.Win32;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WPF.ViewModel
{
    public class MyViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<TreeViewItem> HierarchicalAreas { get; set; }
        public string PathVariable { get; set; }
        public Visibility ChangeControlVisibility { get; set; } = Visibility.Hidden;
        public ICommand Click_Browse { get; }
        public ICommand Click_Button { get; }
        public Reflector reflector { get; set; }

        public MyViewModel()
        {
            HierarchicalAreas = new ObservableCollection<TreeViewItem>();
            Click_Button = new RelayCommand(LoadDLL);
            Click_Browse = new RelayCommand(Browse);
            reflector = new Reflector();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName_)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName_));
        }

        private void LoadDLL()
        {
            if (PathVariable.Substring(PathVariable.Length - 4) == ".dll")
            {
                reflector.Reflect(PathVariable);
                TreeViewLoaded();
            }
        }
        private void TreeViewLoaded()
        {
            TreeViewItem rootItem = new TreeViewItem(reflector) { Name = PathVariable.Substring(PathVariable.LastIndexOf('\\') + 1) };           
            HierarchicalAreas.Add(rootItem);
        }
        private void Browse()
        {
            OpenFileDialog test = new OpenFileDialog()
            {
                Filter = "Dynamic Library File(*.dll)| *.dll"
            };
            test.ShowDialog();
            if (test.FileName.Length == 0)
                MessageBox.Show("No files selected");
            else
            {
                PathVariable = test.FileName;
                ChangeControlVisibility = Visibility.Visible;
                RaisePropertyChanged("ChangeControlVisibility");
                RaisePropertyChanged("PathVariable");
            }
        }
    }
}
