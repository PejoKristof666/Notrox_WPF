using Notrox;
using Notrox.Model;
using Notrox.ViewModel;
using System.Windows;

public class EditAddressesViewModel : ViewModelBase
{
    private UsersClass _user;

    public string ShippingCity { get; set; }
    public int ShippingZip { get; set; }
    public string ShippingAddress { get; set; }

    public string BillingCity { get; set; }
    public int BillingZip { get; set; }
    public string BillingAddress { get; set; }

    public EditAddressesViewModel(UsersClass user)
    {
        _user = user;
        LoadAddresses();
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

                OnPropertyChanged(nameof(ShippingCity));
                OnPropertyChanged(nameof(ShippingZip));
                OnPropertyChanged(nameof(ShippingAddress));
                OnPropertyChanged(nameof(BillingCity));
                OnPropertyChanged(nameof(BillingZip));
                OnPropertyChanged(nameof(BillingAddress));
            }
        }
        catch
        {
            MessageBox.Show("Failed to load addresses");
        }
    }
}