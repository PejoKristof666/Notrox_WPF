using Notrox.Model;
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
    /// Interaction logic for EditAddressesWindow.xaml
    /// </summary>
    public partial class EditAddressesWindow : Window
    {
        public EditAddressesWindow(UsersClass user)
        {
            InitializeComponent();
            DataContext = new EditAddressesViewModel(user);
        }
    }
}
