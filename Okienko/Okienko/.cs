using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Okienko
{
    class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            FetchDataCommend = new RelayCommand(() => DataLayer = new DataLayer());
            DisplayTextCommand = new RelayCommand(ShowPopupWindow, () => !string.IsNullOrEmpty(m_ActionText));
            m_ActionText = "Text to be displayed on the popup";
        }
        public ObservableCollection<User> Users
        {
            get { return m_Users; }
            set
            {
                m_Users = value;
                RaisePropertyChanged();
            }
        }
        public User CurrentUser
        {
            get
            {
                return m_CurrentUser;
            }
            set
            {
                m_CurrentUser = value;
                RaisePropertyChanged();
            }
        }
        public string ActionText
        {
            get { return m_ActionText; }
            set
            {
                m_ActionText = value;
                DisplayTextCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged();
            }
        }
        public RelayCommand DisplayTextCommand
        {
            get;
            private set;
        }
        public RelayCommand FetchDataCommend
        {
            get; private set;
        }

        public Func<string, string, MessageBoxButton, MessageBoxImage, MessageBoxResult> MessageBoxShowDelegate { get; set; } = MessageBox.Show;
        public DataLayer DataLayer
        {
            get { return m_DataLayer; }
            set
            {
                m_DataLayer = value; Users = new ObservableCollection<User>(value.User);
            }
        }

        private DataLayer m_DataLayer;
        private User m_CurrentUser;
        private string m_ActionText;
        private ObservableCollection<User> m_Users;
        private void ShowPopupWindow()
        {
            MessageBoxShowDelegate(ActionText, "Button interaction", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
