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
    public class Data2ViewModel : IDataViewModel
    {
        private Data2ViewModel(Data2View _view)
        {
            view = _view;
            view.DataContext = this;

            FileName = "文件2.txt";
            Title = "文件2";

            Datas = new ObservableCollection<FileData>();

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title ="Y1",
                    Values =new ChartValues<ObservablePoint>(),
                    Fill = new SolidColorBrush(Color.FromArgb(0,0,0,0)),
                    StrokeThickness = 1,
                    PointGeometrySize = 0
                }
            };
        }

        private readonly Data2View view;

        private static Data2ViewModel _instance = null;
        private static readonly object SynObject = new object();

        public event PropertyChangedEventHandler PropertyChanged;

        public static IDataViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SynObject)
                    {
                        if (_instance == null)
                        {
                            _instance = new Data2ViewModel(new Data2View());
                        }
                    }
                }

                return _instance;
            }
        }

        public ObservableCollection<FileData> Datas { get; set; }

        public string Title { get; set; }

        public string FileName { get; set; }

        private int _loadPrecent;
        public int LoadPrecent
        {
            get
            {
                return _loadPrecent;
            }
            set
            {
                _loadPrecent = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LoadPrecent"));
            }
        }

        public SeriesCollection SeriesCollection { get; set; }

        public UserControl GetView()
        {
            return view;
        }
    }
}
