using CommunityToolkit.Mvvm.Input;
using Notrox.Model;
using Notrox.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Notrox.ViewModel
{
    public class AddProductViewModel : ViewModelBase
    {
        public ObservableCollection<CompanyClass> Companies { get; set; } = new();

        private string _productName;
        private string _description;
        private int _price;
        private int _ammount;
        private int _selectedcompanyid;
        private string _imgurl;

        public string ProductName { get => _productName; set { _productName = value; OnPropertyChanged(); } }
        public string Description { get => _description; set { _description = value; OnPropertyChanged(); } }
        public int Price { get => _price; set { _price = value; OnPropertyChanged(); } }
        public int Ammount { get => _ammount; set { _ammount = value; OnPropertyChanged(); } }
        public int SelectedCompanyId { get => _selectedcompanyid; set { _selectedcompanyid = value; OnPropertyChanged(); } }
        public string IMGURL { get => _imgurl; set { _imgurl  = value; OnPropertyChanged(); } }

        public AsyncRelayCommand<Window> CreateProduct { get; }

        public AddProductViewModel()
        {
            Companies.Clear();

            Companies = new ObservableCollection<CompanyClass>{
                new CompanyClass { Id = 1, Name = "SteelSeries" },
                new CompanyClass { Id = 2, Name = "Logitech" },
                new CompanyClass { Id = 3, Name = "HyperX" },
                new CompanyClass { Id = 4, Name = "Secretlab" },
                new CompanyClass { Id = 5, Name = "Endorfy" },
                new CompanyClass { Id = 6, Name = "Razer" },
                new CompanyClass { Id = 7, Name = "ASUS" }
            };

            CreateProduct = new AsyncRelayCommand<Window>(CreateProductF);
        }

        private async Task CreateProductF(Window window)
        {
            var success = await App.Server.AddProduct(
                ProductName,
                Description,
                Price,
                Ammount,
                SelectedCompanyId,
                IMGURL
            );

            if (success)
            {
                MessageBox.Show("Product created!");
                window.DialogResult = true;
                window.Close();

            }
            else
            {
                MessageBox.Show("Failed Add / Product");
            }
        }
    }
}
