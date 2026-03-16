using CommunityToolkit.Mvvm.Input;
using Notrox.Model;
using Notrox.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Notrox.ViewModel
{
    public class ProductsViewModel : ViewModelBase
    {
        public ObservableCollection<ProductsClass> Products { get; set; } = new();
        public RelayCommand<ProductsClass> EditProduct { get; }
        public RelayCommand<ProductsClass> DeleteProduct { get; }

        public ProductsViewModel()
        {
            LoadProducts();

            EditProduct = new RelayCommand<ProductsClass>(OpenEditProductsF);
            DeleteProduct = new RelayCommand<ProductsClass>(DeleteProductF);
        }

        private async void LoadProducts()
        {
            var products = await App.Server.ListProducts();

            Products.Clear();

            foreach (var item in products) Products.Add(item);
        }

        private void OpenEditProductsF(ProductsClass product)
        {
            EditProductsWindow window = new EditProductsWindow(product);
            window.ShowDialog();
        }

        private async void DeleteProductF(ProductsClass product)
        {
            if (product == null) return;

            var result = MessageBox.Show($"Delete product {product.Name}?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result != MessageBoxResult.Yes) return;

            bool success = await App.Server.DeleteProduct(product.Id);

            if (success)
            {
                Products.Remove(product);
            }

            else
            {
                MessageBox.Show("Failed Delete / Product");
            }
        }
    }
}
