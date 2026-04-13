using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using CommunityToolkit.Mvvm.Input;
using Notrox.Interfaces;
using Notrox.Model;
using Notrox.View;

namespace Notrox.ViewModel
{
    public class OrdersViewModel : ViewModelBase
    {
        public ObservableCollection<OrdersClass> Orders { get; set; } = new();
        public ObservableCollection<string> Chooses { get; set; } = new();
 
        private CompanyClass _selectedPhase;
        public CompanyClass SelectedPhase { get => _selectedPhase; set { _selectedPhase = value; OnPropertyChanged(); } }
        public ICollectionView OrdersView { get; set; }

        private string _searchText;
        public RelayCommand<OrdersClass> EditOrder { get; }

        public OrdersViewModel()
        {
            Chooses = new ObservableCollection<string>
            {
                "Feldolgozás alatt",
                "Összekészítés alatt",
                "Csomagolás kész",
                "Átadva futárnak",
                "Kiszállítás alatt",
                "Kiszállítva",
                "Törölve"
            };

            OrdersView = CollectionViewSource.GetDefaultView(Orders);

            LoadOrders();

            EditOrder = new RelayCommand<OrdersClass>(async order =>
            {
                if (order == null) return;

                bool success = await App.Server.EditOrder(order.Id, order.SelectedPhase);
                if (success)
                {
                    MessageBox.Show($"Termék #{order.Id} sikeresen módosítva");
                }
                else
                {
                    MessageBox.Show($"Nem sikerült módosítani: #{order.Id}");
                }
            });
        }
        private async void LoadOrders()
        {
            var products = await App.Server.ListOrders();

            Orders.Clear();

            foreach (var item in products)
            {
                item.SelectedPhase = item.Phase;
                Orders.Add(item);
            }
            
        }
    }
}
