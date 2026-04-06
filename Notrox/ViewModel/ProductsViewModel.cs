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
using System.ComponentModel;
using System.Windows.Data;
using Notrox.Interfaces;

namespace Notrox.ViewModel
{
    public class ProductsViewModel : ViewModelBase, ISearchable
    {
        public ObservableCollection<ProductsClass> Products { get; set; } = new();
        public ICollectionView ProductsView { get; set; }

        private string _searchText;
        public RelayCommand<ProductsClass> EditProduct { get; }
        public RelayCommand<ProductsClass> DeleteProduct { get; }

        public ProductsViewModel()
        {
            ProductsView = CollectionViewSource.GetDefaultView(Products);
            ProductsView.Filter = FilterProducts;

            LoadProducts();

            EditProduct = new RelayCommand<ProductsClass>(OpenEditProductsF);
            DeleteProduct = new RelayCommand<ProductsClass>(DeleteProductF);
        }

        public async void LoadProducts()
        {
            var products = await App.Server.ListProducts();

            Products.Clear();

            foreach (var item in products) Products.Add(item);
        }

        private void OpenEditProductsF(ProductsClass product)
        {
            EditProductsWindow window = new EditProductsWindow(product);

            if (window.ShowDialog() == true)
            {
                LoadProducts();
            }
        }

        private async void DeleteProductF(ProductsClass product)
        {
            if (product == null) return;

            var result = MessageBox.Show($"Termék törlése: {product.Name}?", "Biztos törölni akarod?", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result != MessageBoxResult.Yes) return;

            bool success = await App.Server.DeleteProduct(product.Id);

            if (success)
            {
                Products.Remove(product);
            }

            else
            {
                MessageBox.Show("Sikertelen törlés");
            }
        }

        private bool FilterProducts(object obj)
        {
            if(obj is not ProductsClass product) return false;
            if(string.IsNullOrWhiteSpace(_searchText)) return true;

            return product.Name.Contains(_searchText, StringComparison.OrdinalIgnoreCase) || product.Description.Contains(_searchText, StringComparison.OrdinalIgnoreCase);

        }

        public void ApplySearch(string text)
        {
            _searchText = text;
            ProductsView.Refresh();
        }
    }
}
