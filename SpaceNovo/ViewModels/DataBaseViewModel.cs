using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceNovo.Views;
using SpaceNovo.Interface;
using System.Windows.Controls;
using SpaceNovo.Domain;
using System.Collections.ObjectModel;
using SpaceNovo.Models;
using SpaceNovo.Helper;
using System.Threading;
using System.Windows;
using System.IO;
using LiveCharts;
using LiveCharts.Wpf;
using System.Windows.Media;
using LiveCharts.Configurations;
using LiveCharts.Defaults;
using System.ComponentModel;

namespace SpaceNovo.ViewModels
{
    public class DataBaseViewModel : IViewModel
    {
        private DataBaseViewModel(DataBaseView _view)
        {
            view = _view;
            view.DataContext = this;

            HomeCmd = new RelayCommand(MainWindowViewModel.Instance.Home);

            LoadCmd = new RelayCommand(Load);
        }

        private readonly DataBaseView view;
        private static DataBaseViewModel _instance = null;
        private static readonly object SynObject = new object();

        public event PropertyChangedEventHandler PropertyChanged;

        public static DataBaseViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SynObject)
                    {
                        if (_instance == null)
                        {
                            _instance = new DataBaseViewModel(new DataBaseView());
                        }
                    }
                }

                return _instance;
            }
        }

        public RelayCommand HomeCmd { get; set; }

        public RelayCommand LoadCmd { get; set; }

        private object _view;
        public object View
        {
            get { return _view; }
            set
            {
                _view = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("View"));
            }
        }

        private IDataViewModel _childViewModel;
        public IDataViewModel ChildViewModel
        {
            get { return _childViewModel; }
            set
            {
                _childViewModel = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ChildViewModel"));
            }
        }

        public void ViewChange(int ViewIndex)
        {
            MainWindowViewModel.Instance.ActiveItem = view;

            switch (ViewIndex)
            {
                case 1:
                    ChildViewModel = Data1ViewModel.Instance;
                    break;
                case 2:
                    ChildViewModel = Data2ViewModel.Instance;
                    break;
                case 3:
                    ChildViewModel = Data3ViewModel.Instance;
                    break;
                case 4:
                    ChildViewModel = Data4ViewModel.Instance;
                    break;
                default:
                    break;
            }

            View = ChildViewModel.GetView();
        }

        public UserControl GetView()
        {
            return view;
        }

        public async void Load(object parameter)
        {
            await Task.Run(() =>
            {
                for (int i = 0; i < ChildViewModel.SeriesCollection.Count; i++)
                {
                   ChildViewModel.SeriesCollection[i].Values.Clear();
                }

                ChildViewModel.LoadPrecent = 0;

                Application.Current.Dispatcher.Invoke(() => ChildViewModel.Datas.Clear());

                FileReadHelper.ReadFile(Environment.CurrentDirectory + "\\Files\\" + ChildViewModel.FileName, OnReadFile);
            });
        }

        public void OnReadFile(FileData fileData, double percent)
        {
            if (fileData != null)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    ChildViewModel.Datas.Add(fileData);
                });

                for (int i = 0; i < ChildViewModel.SeriesCollection.Count; i++)
                {
                    var s = ChildViewModel.SeriesCollection[i];

                    s.Values.Add(new ObservablePoint(fileData.X, fileData.YS[i]));
                }

                ChildViewModel.LoadPrecent = (int)(percent * 100);
            }
            else
                ChildViewModel.LoadPrecent = 100;

            Thread.Sleep(100);
        }
    }
}
