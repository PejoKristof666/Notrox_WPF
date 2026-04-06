using CommunityToolkit.Mvvm.Input;
using Notrox.Interfaces;
using Notrox.Model;
using Notrox.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Notrox.ViewModel
{
    public class UsersViewModel : ViewModelBase, ISearchable
    {
        public ObservableCollection<UsersClass> Users { get; set; } = new();

        public ICollectionView UsersView { get; set; }

        private string _searchText;

        public UsersViewModel()
        {
            UsersView = CollectionViewSource.GetDefaultView(Users);
            UsersView.Filter = FilterUsers;


            LoadUsers();

            ViewAddresses = new RelayCommand<UsersClass>(OpenViewAddressesF);
            DeleteUser = new RelayCommand<UsersClass>(DeleteUserFunction);
        }

        public RelayCommand<UsersClass> ViewAddresses { get; }
        public RelayCommand<UsersClass> DeleteUser { get; }

        private async void LoadUsers()
        {
            var users = await App.Server.ListUsers();

            Users.Clear();

            foreach (var u in users) Users.Add(u);
        }

        private void OpenViewAddressesF(UsersClass user)
        {
            ViewAddressesWindow window = new ViewAddressesWindow(user);
            if (window.ShowDialog() == true)
            {
                LoadUsers();
            }
        }

        private async void DeleteUserFunction(UsersClass user)
        {
            if (user == null) return;

            var result = MessageBox.Show($"Felhasználó törlése: {user.Username}?", "Biztos törölni akarod?", MessageBoxButton.YesNo,MessageBoxImage.Warning);

            if (result != MessageBoxResult.Yes) return;

            bool success = await App.Server.DeleteUser(user.Id);

            if (success)
            {
                Users.Remove(user);
            }
            else
            {
                MessageBox.Show("Sikertelen törlés");
            }
        }

        private bool FilterUsers(object obj)
        {
            if (obj is not UsersClass user) return false;
            if (string.IsNullOrWhiteSpace(_searchText)) return true;

            return user.Username.Contains(_searchText, StringComparison.OrdinalIgnoreCase);

        }

        public void ApplySearch(string text)
        {
            _searchText = text;
            UsersView.Refresh();
        }
    }
}
