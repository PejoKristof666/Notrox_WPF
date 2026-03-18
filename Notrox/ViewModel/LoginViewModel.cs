using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Notrox.Services;

using CommunityToolkit.Mvvm.Input;

using Notrox.View;

namespace Notrox.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private string _username;

        public string Username
        {
            get => _username; set { _username = value; OnPropertyChanged(); }
        }

        public AsyncRelayCommand<object> LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new AsyncRelayCommand<object>(Login);
        }

        private async Task Login(object param)
        {
            if (param is not PasswordBox passwordBox) return;

            string password = passwordBox.Password;

            bool success = await App.Server.Login(Username, password);

            if (!success)
            {
                MessageBox.Show("Hibás felhasználónév vagy jelszó."); return;
            }

            var user = await App.Server.GetCurrentUser();

            if (user == null)
            {
                MessageBox.Show("Nincs admin jogosultságod!"); return;
            }

            Session.SetUsername = user.Username;

            AdminWindow admin = new AdminWindow();
            admin.Show();

            Application.Current.MainWindow.Close();
        }
    }
}