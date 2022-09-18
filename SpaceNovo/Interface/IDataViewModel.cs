using LiveCharts;
using SpaceNovo.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceNovo.Interface
{
    public interface IDataViewModel : IViewModel
    {
        string Title { get; set; }

        string FileName { get; set; }

        int LoadPrecent { get; set; }

        ObservableCollection<FileData> Datas { get; set; }

        SeriesCollection SeriesCollection { get; set; }
    }
}
