using CommunityToolkit.Mvvm.Input;
using Notrox.Model;
using Notrox.Services;
using Notrox.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Data;
using Notrox.Interfaces;

namespace Notrox.ViewModel

{
    public class AdminViewModel : ViewModelBase
    {
        private object _currentView;
        public object CurrentView { get => _currentView; set { _currentView = value; OnPropertyChanged(); } }

        private string _searchText;
        public string SearchText { get => _searchText; set { _searchText = value; OnPropertyChanged(); if (CurrentView is ISearchable searchable) searchable.ApplySearch(_searchText); } }
        

        public RelayCommand ShowUsers { get; }
        public RelayCommand ShowOrders { get; }
        public RelayCommand ShowProducts { get; }
        public RelayCommand AddProduct { get; }

        public AdminViewModel()
        {
            ShowUsers = new RelayCommand(() => { CurrentView = new UsersViewModel(); ApplySearchToCurrent(); });
            ShowOrders = new RelayCommand(() => { CurrentView = new OrdersViewModel(); ApplySearchToCurrent(); });
            ShowProducts = new RelayCommand(() => { CurrentView = new ProductsViewModel(); ApplySearchToCurrent(); });

            CurrentView = new UsersViewModel();

            AddProduct = new RelayCommand(OpenAddProductWindowF);
        }

        private void OpenAddProductWindowF()
        {
            AddProductWindow window = new AddProductWindow();
            window.ShowDialog();
        }

        private void ApplySearchToCurrent()
        {
            if(CurrentView is ISearchable searchable) searchable.ApplySearch(SearchText);
        }
        public string AdminName => Session.SetUsername;
    }
}
