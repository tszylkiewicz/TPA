using Model.ViewModel.TreeView;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System;
using Model.Model;
using MEF;
using System.Configuration;

namespace Model.ViewModel
{
    public class MyViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<TreeViewItem> HierarchicalAreas { get; set; }
        public string PathVariable { get; set; }
        public ICommand Click_Browse { get; }
        public ICommand Click_Button { get; }
        public ICommand Click_Save { get; }
        public ICommand Click_Read { get; }
        public IBrowseFile BrowseFile { get; set; }
        public Reflector reflector { get; set; }
        public TreeViewAssembly treeViewAssembly;
        public string PathForSerialization { get; set; }

        public Logic LogicService { get; set; }
        private string _compositionPath = ConfigurationManager.AppSettings["pluginsPath"];

        [Import(typeof(ILogWriter))]
        public ILogWriter Logger { get; set; }

        public MyViewModel()
        {
            HierarchicalAreas = new ObservableCollection<TreeViewItem>();
            Click_Button = new RelayCommand(LoadDLL);
            Click_Browse = new RelayCommand(Browse);
            Click_Save = new RelayCommand(Save);
            Click_Read = new RelayCommand(Read);
            reflector = new Reflector();
            Compose();
            LogicService = new Logic();
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
            Logger.LogIt(new LogWriter("SavingToFile"));

            if (BrowseFile != null)
            {
                PathForSerialization = BrowseFile.SavePath();
            }
            if (PathForSerialization != null)
            {
                try
                {
                    reflector.AssemblyModel.Save(PathForSerialization, LogicService);
                }
                catch(Exception)
                {
                    Logger.LogIt(new LogWriter("Error"));
                }
            }
        }

        public void Read()
        {
            Logger.LogIt(new LogWriter("Reading"));
            if (BrowseFile != null)
            {
                PathForSerialization = BrowseFile.ChooseFile();
                Console.WriteLine(PathForSerialization);
            }
            if (PathForSerialization != null)
            {
                try
                {
                    // AssemblyMetadata asm = LogicService.Load(PathForSerialization);
                    AssemblyMetadata asm = reflector.AssemblyModel.Load(PathForSerialization, LogicService);
                    treeViewAssembly = new TreeViewAssembly(asm);
                    TreeViewLoaded();
                }
                catch (Exception)
                {
                    Logger.LogIt(new LogWriter("Error"));
                }
            }
        }

        private void Compose()
        {        
            var catalog = new AggregateCatalog(new DirectoryCatalog(_compositionPath));
            var _container = new CompositionContainer(catalog);
            try
            {
                _container.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                throw compositionException; 
            }
            Logger.LogIt(new LogWriter("Compose"));
        }
    }
}
