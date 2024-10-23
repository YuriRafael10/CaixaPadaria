using CaixaPadaria.Views;
using System.Configuration;
using System.Data;
using System.Windows;

namespace CaixaPadaria
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new LoginWindow();
            MainWindow.Show();
            base.OnStartup(e);
        }
    }

}
