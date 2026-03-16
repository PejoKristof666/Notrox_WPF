using CommunityToolkit.Mvvm.Input;
using Notrox.Model;
using Notrox.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Notrox.ViewModel
{
    public class EditProductsViewModel : ViewModelBase
    {
        private ProductsClass _product;

        private string productName;
        public string ProductName { get => productName; set { productName = value; OnPropertyChanged(); } }

        
        private string description;
        public string Description { get => description; set { description = value; OnPropertyChanged(); } }


        private int price;
        public int Price { get => price; set { price = value; OnPropertyChanged(); } }


        private int ammount;
        public int Ammount { get => ammount; set { ammount = value; OnPropertyChanged(); } }


        private string imgurl;
        public string IMGURL { get => imgurl; set { imgurl = value; OnPropertyChanged(); } }

        public ICommand SaveCommand { get; }

        public EditProductsViewModel(ProductsClass product)
        {
            _product = product;

            SaveCommand = new RelayCommand(SaveProductEdit);

            LoadProductsInfos();
        }

        private async void SaveProductEdit()
        {
            try
            {
                bool updateProduct = await App.Server.EditProduct(_product.Id ,ProductName, Description, Price, Ammount, IMGURL);

                if (updateProduct)
                {
                    MessageBox.Show("Product updated successfully");
                }
            }
            catch
            {
                MessageBox.Show("Update Failed / Product");
            }
        }

        private void LoadProductsInfos()
        {
            try
            {
                ProductName = _product.Name;
                Description = _product.Description;
                Price = _product.Price;
                Ammount = _product.Ammount;
                IMGURL = _product.IMGURL;
            }
            catch
            {
                MessageBox.Show("Load Failed / Product");
            }
        }
    }
}
