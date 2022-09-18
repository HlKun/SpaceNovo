using SpaceNovo.Domain;
using SpaceNovo.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SpaceNovo.ViewModels
{
    public class LoginViewModel: INotifyPropertyChanged
    {
        private LoginViewModel(LoginView _loginView)
        {
            loginView = _loginView;
            loginView.DataContext = this;

            CurrentDate = DateTime.Now;

            CloseCmd = new RelayCommand(Close);
            LoginCmd = new RelayCommand(Login);
        }

        private readonly LoginView loginView;

        private static LoginViewModel _instance = null;
        private static readonly object SynObject = new object();

        public event PropertyChangedEventHandler PropertyChanged;

        public static LoginViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SynObject)
                    {
                        if (_instance == null)
                        {
                            _instance = new LoginViewModel(new LoginView());
                        }
                    }
                }

                return _instance;
            }
        }

        private DateTime currentDate;
        public DateTime CurrentDate
        {
            get
            {
                return currentDate;
            }
            set
            {
                currentDate = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentDate"));
            }
        }

        public RelayCommand CloseCmd { get; set; }

        public RelayCommand LoginCmd { get; set; }

        public void Login(object parameter)
        {
            loginView.Hide();
            MainWindowViewModel.Instance.Show();
        }

        public void Close(object parameter)
        {
            loginView.Close();
        }

        public void Show()
        {
            loginView.Show();
        }
    }
}
