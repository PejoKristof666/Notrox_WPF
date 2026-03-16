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

namespace Notrox.ViewModel
{
    public class UsersViewModel : ViewModelBase
    {
        public ObservableCollection<UsersClass> Users { get; set; } = new();

        public UsersViewModel()
        {
            LoadUsers();

            
            EditAddresses = new RelayCommand<UsersClass>(OpenEditAddressesF);
            DeleteUser = new RelayCommand<UsersClass>(DeleteUserFunction);
        }

        public RelayCommand<UsersClass> EditAddresses { get; }
        public RelayCommand<UsersClass> DeleteUser { get; }

        private async void LoadUsers()
        {
            var users = await App.Server.ListUsers();

            Users.Clear();

            foreach (var u in users) Users.Add(u);
        }

        private void OpenEditAddressesF(UsersClass user)
        {
            EditAddressesWindow window = new EditAddressesWindow(user);
            window.ShowDialog();
        }

        private async void DeleteUserFunction(UsersClass user)
        {
            if (user == null) return;

            var result = MessageBox.Show($"Delete user {user.Username}?","Confirm Delete",MessageBoxButton.YesNo,MessageBoxImage.Warning);

            if (result != MessageBoxResult.Yes) return;

            bool success = await App.Server.DeleteUser(user.Id);

            if (success)
            {
                Users.Remove(user);
            }
            else
            {
                MessageBox.Show("Failed to delete user.");
            }
        }
    }
}
