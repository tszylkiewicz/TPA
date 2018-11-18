﻿using Microsoft.Win32;
using Model.Model;
using Model.ViewModel;
using Model.ViewModel.TreeView;
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


namespace Model.ViewModel
{
    public class MyViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<TreeViewItem> HierarchicalAreas { get; set; }
        public string PathVariable { get; set; }
        public ICommand Click_Browse { get; }
        public ICommand Click_Button { get; }
        public IBrowseFile BrowseFile { get; set; }
        public Reflector reflector { get; set; }
        private TreeViewAssembly treeViewAssembly;

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
                treeViewAssembly = new TreeViewAssembly(reflector.AssemblyModel);
                TreeViewLoaded();
            }
        }
        private void TreeViewLoaded()
        {
            TreeViewItem rootItem = treeViewAssembly;           
            HierarchicalAreas.Add(rootItem);
        }
        private void Browse()
        {
            PathVariable = BrowseFile.ChooseFile();
            RaisePropertyChanged("PathVariable");
        }
    }
}
