using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Defaults;
using SpaceNovo.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SpaceNovo.ViewModels
{
    public class MainWindowViewModel: INotifyPropertyChanged
    {
        private MainWindowViewModel()
        {
            var customerVmMapper = Mappers.Xy<ObservablePoint>()
              .X(value => value.X)
              .Y(value => value.Y);

            Charting.For<ObservablePoint>(customerVmMapper);

            CloseCmd = new RelayCommand(Close);
            activeItem = MenuViewModel.Instance.GetView();
        }

        private MainWindow mainWindow;

        private static MainWindowViewModel _instance = null;
        private static readonly object SynObject = new object();

        public event PropertyChangedEventHandler PropertyChanged;

        public static MainWindowViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SynObject)
                    {
                        if (_instance == null)
                        {
                            _instance = new MainWindowViewModel();
                        }
                    }
                }

                return _instance;
            }
        }

        public bool LoginState { get; set; } = false;

        private object activeItem;
        public object ActiveItem
        {
            get { return activeItem; }
            set
            {
                activeItem = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ActiveItem"));
            }
        }

        public RelayCommand CloseCmd { get; set; }

        public void SetWindow(MainWindow _mainWindow)
        {
            mainWindow = _mainWindow;
        }

        public void Home(object parameter)
        {
            ActiveItem = MenuViewModel.Instance.GetView();
        }

        public void Close(object parameter)
        {
            mainWindow.Close();
        }

        public void Show()
        {
            mainWindow.Show();
        }
    }
}
