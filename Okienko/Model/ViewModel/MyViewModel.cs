using Model.ViewModel.TreeView;
using XMLSerializer;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;


namespace Model.ViewModel
{
    public class MyViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<TreeViewItem> HierarchicalAreas { get; set; }
        public string PathVariable { get; set; }
        public ICommand Click_Browse { get; }
        public ICommand Click_Button { get; }
        public ICommand Click_Save { get; }
        public IBrowseFile BrowseFile { get; set; }
        public Reflector reflector { get; set; }
        public TreeViewAssembly treeViewAssembly;
        public ISerializer Serializer = new XMLSerializer.XMLSerializer();
        public string PathForSerialization { get; set; }

        public MyViewModel()
        {
            HierarchicalAreas = new ObservableCollection<TreeViewItem>();
            Click_Button = new RelayCommand(LoadDLL);
            Click_Browse = new RelayCommand(Browse);
            Click_Save = new RelayCommand(Save);
            reflector = new Reflector();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName_)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName_));
        }

        public void LoadDLL()
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
        public void Save()
        {
            if (BrowseFile != null)
            {
                PathForSerialization = BrowseFile.SavePath();
            }
            if (PathForSerialization != null)
            {
                Serializer.Serialize(PathForSerialization, reflector.AssemblyModel);
            }
        }
        public void saveAssemblyToXml(Reflector reflectorTemp = null)
        {
            if (reflectorTemp == null)
            {
                Save();
            }
            else
            {
                Serializer.Serialize(PathForSerialization, reflectorTemp.AssemblyModel);
            }
        }
    }
}
