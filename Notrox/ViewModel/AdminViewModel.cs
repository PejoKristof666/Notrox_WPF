using CommunityToolkit.Mvvm.Input;
using Notrox.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notrox.ViewModel
{
    public class AdminViewModel : ViewModelBase
    {
        private object _currentView;

        public object CurrentView
        {
            get => _currentView; set { _currentView = value; OnPropertyChanged(); }
        }

        public RelayCommand ShowUsers { get; }
        public RelayCommand ShowOrders { get; }
        public RelayCommand ShowProducts { get; }

        public AdminViewModel()
        {
            ShowUsers = new RelayCommand(() => CurrentView = new UsersViewModel());
            ShowOrders = new RelayCommand(() => CurrentView = new OrdersViewModel());
            ShowProducts = new RelayCommand(() => CurrentView = new ProductsViewModel());

            CurrentView = new UsersViewModel();
        }

        public string AdminName => Session.SetUsername;
    }
}
