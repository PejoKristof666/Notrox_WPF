using Notrox.Services;
using Notrox.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Notrox.View
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            DataContext = new AdminViewModel();
        }
        private void Logout(object sender, RoutedEventArgs e)
        {
            App.Server.Logout();
            Session.SetUsername = null;

            MainWindow loginwindow = new MainWindow();
            loginwindow.Show();

            this.Close();
        }
    }
}