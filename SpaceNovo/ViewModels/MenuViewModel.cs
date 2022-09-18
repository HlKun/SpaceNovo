using SpaceNovo.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceNovo.Domain;
using SpaceNovo.Interface;
using System.Windows.Controls;
using System.ComponentModel;

namespace SpaceNovo.ViewModels
{
    public class MenuViewModel : IViewModel
    {
        private MenuViewModel(MenuView _view)
        {
            view = _view;
            view.DataContext = this;

            ViewChangeCmd = new RelayCommand(ViewChange);
        }

        private readonly MenuView view;

        private static MenuViewModel _instance = null;
        private static readonly object SynObject = new object();

        public event PropertyChangedEventHandler PropertyChanged;

        public static MenuViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SynObject)
                    {
                        if (_instance == null)
                        {
                            _instance = new MenuViewModel(new MenuView());
                        }
                    }
                }

                return _instance;
            }
        }

        public RelayCommand ViewChangeCmd { get; set; }

        public void ViewChange(object parameter)
        {
            if (parameter is string strViewIndex)
            {
                int ViewIndex = Convert.ToInt32(strViewIndex);

                DataBaseViewModel.Instance.ViewChange(ViewIndex);
            }
        }

        public UserControl GetView()
        {
            return view;
        }
    }
}
