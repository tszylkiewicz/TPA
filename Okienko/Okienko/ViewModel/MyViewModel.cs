using Microsoft.Win32;
using Okienko.Model;
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

namespace Okienko.ViewModel
{
    public class MyViewModel : INotifyPropertyChanged
    {
        public MyViewModel()
        {
            HierarchicalAreas = new ObservableCollection<TreeViewItem>();
            Click_Button = new RelayCommand(LoadDLL);
            Click_Browse = new RelayCommand(Browse);
            reflector = new Reflector();
        }

        public ObservableCollection<TreeViewItem> HierarchicalAreas { get; set; }
        public string PathVariable { get; set; }
        public Visibility ChangeControlVisibility { get; set; } = Visibility.Hidden;
        public ICommand Click_Browse { get; }
        public ICommand Click_Button { get; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName_)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName_));
        }

        private void LoadDLL()
        {
            if (PathVariable.Substring(PathVariable.Length - 4) == ".dll")
            {
                Console.WriteLine("uuuaaa\n\n"+PathVariable+"\n\nuuuaa");
                reflector.Reflect(PathVariable);
                TreeViewLoaded();
            }
        }
        private void TreeViewLoaded()
        {
            TreeViewItem rootItem = new TreeViewItem(reflector) { Name = PathVariable.Substring(PathVariable.LastIndexOf('\\') + 1) };           
            HierarchicalAreas.Add(rootItem);
            Reflect();
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


            //////////////////
            //Console.WriteLine(PathVariable);
            //////////////////
        }


        //Reflection
        public void Reflect()
        {
            Assembly assembly = Assembly.LoadFrom(PathVariable);
            foreach(Type typ in assembly.GetTypes())
                Console.WriteLine(typ.GetProperties());
            Console.WriteLine(PathVariable);
            Console.WriteLine(reflector.m_AssemblyModel.m_Name);
            foreach (TypeMetadata type in reflector.m_AssemblyModel.Types)
            {
                Console.WriteLine(type.m_NamespaceName);
                Console.WriteLine(type.m_typeName);
            }
                //for(int i=0; i< assembly.GetTypes().Count(); i++)
            //Console.WriteLine(assembly.GetTypes()[i].Namespace);
        }

        public Reflector reflector { get; set; }

    }
}
