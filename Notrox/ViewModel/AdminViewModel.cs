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
using System.Windows.Input;
using Microsoft.Win32;
using System.Windows;
using System.IO;

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
        public RelayCommand SaveToCSV { get; }
        public AdminViewModel()
        {
            ShowUsers = new RelayCommand(() => { SearchText = string.Empty; CurrentView = new UsersViewModel(); ApplySearchToCurrent(); });
            ShowOrders = new RelayCommand(() => { SearchText = string.Empty; CurrentView = new OrdersViewModel(); ApplySearchToCurrent(); });
            ShowProducts = new RelayCommand(() => { SearchText = string.Empty; CurrentView = new ProductsViewModel(); ApplySearchToCurrent(); });

            CurrentView = new UsersViewModel();

            AddProduct = new RelayCommand(OpenAddProductWindowF);

            SaveToCSV = new RelayCommand(SaveToCSVFunction);
        }

        private void OpenAddProductWindowF()
        {
            AddProductWindow window = new AddProductWindow();

            if (window.ShowDialog() == true)
            {
                if (CurrentView is ProductsViewModel productsVM)
                {
                    productsVM.LoadProducts();
                }
            }
        }

        private void ApplySearchToCurrent()
        {
            if(CurrentView is ISearchable searchable) searchable.ApplySearch(SearchText);
        }

        private void SaveToCSVFunction()
        {
            if (CurrentView is not UsersViewModel usersVM)
            {
                MessageBox.Show("You must be on the Users page to export CSV."); return;
            }

            var dialog = new SaveFileDialog
            {
                FileName = $"users_{DateTime.Now:yyyyMMdd_HHmmss}",
                DefaultExt = ".txt",
                Filter = "Text files (*.txt)|*.txt"
            };

            if (dialog.ShowDialog() == true)
            {
                var csv = ConvertToCsv(usersVM.Users);
                File.WriteAllText(dialog.FileName, csv);
            }
        }

        private string ConvertToCsv(IEnumerable<UsersClass> users)
        {
            var sb = new StringBuilder();

            sb.AppendLine("ID,Username,Email");

            foreach (var user in users)
            {
                sb.AppendLine($"{user.Id},{Escape(user.Username)},{Escape(user.Email)}");
            }

            return sb.ToString();
        }

        private string Escape(string value)
        {
            if (string.IsNullOrEmpty(value)) return "";

            if (value.Contains(",") || value.Contains("\""))
            {
                value = value.Replace("\"", "\"\""); 
                return $"\"{value}\"";
            }

            return value;
        }

        public string AdminName => Session.SetUsername;
    }
}
