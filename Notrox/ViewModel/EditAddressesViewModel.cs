using CommunityToolkit.Mvvm.Input;
using Notrox.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Notrox.ViewModel
{
    public class EditAddressesViewModel : ViewModelBase
    {
        private UsersClass _user;

        private string shippingCity;
        public string ShippingCity { get => shippingCity; set { shippingCity = value; OnPropertyChanged(); } }


        private int shippingZip;
        public int ShippingZip { get => shippingZip; set { shippingZip = value; OnPropertyChanged(); } }


        private string shippingAddress;
        public string ShippingAddress { get => shippingAddress; set { shippingAddress = value; OnPropertyChanged(); } }


        private string billingCity;
        public string BillingCity { get => billingCity; set { billingCity = value; OnPropertyChanged(); } }


        private int billingZip;
        public int BillingZip { get => billingZip; set { billingZip = value; OnPropertyChanged(); } }


        private string billingAddress;
        public string BillingAddress { get => billingAddress; set { billingAddress = value; OnPropertyChanged(); } }


        public ICommand SaveCommand { get; }

        public EditAddressesViewModel(UsersClass user)
        {
            _user = user;

            SaveCommand = new RelayCommand(SaveAddresses);

            LoadAddresses();
        }

        private async void SaveAddresses()
        {
            try
            {
                bool shipping = await App.Server.EditAddress(ShippingCity, ShippingZip, ShippingAddress);
                bool billing = await App.Server.EditBillingAddress(BillingCity, BillingZip, BillingAddress);

                if (shipping && billing)
                {
                    MessageBox.Show("Addresses updated successfully");
                }
                else
                {
                    MessageBox.Show("Failed to update one or more addresses");
                }
            }
            catch
            {
                MessageBox.Show("Error updating addresses");
            }
        }

        private async void LoadAddresses()
        {
            try
            {
                var addresses = await App.Server.GetUserAddresses(_user.Id);

                if (addresses != null)
                {
                    if (addresses.shipping.Count > 0)
                    {
                        ShippingCity = addresses.shipping[0].City;
                        ShippingZip = addresses.shipping[0].Zip;
                        ShippingAddress = addresses.shipping[0].Address1;
                    }

                    if (addresses.billing.Count > 0)
                    {
                        BillingCity = addresses.billing[0].City;
                        BillingZip = addresses.billing[0].Zip;
                        BillingAddress = addresses.billing[0].Address1;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Failed to load addresses");
            }
        }
    }
}
