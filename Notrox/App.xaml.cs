using Notrox.Services;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Notrox
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ServerConnection Server { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Server = new ServerConnection("https://notrox.hu");
        }
    }

}
