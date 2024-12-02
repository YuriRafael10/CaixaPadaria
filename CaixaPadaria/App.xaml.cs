using CaixaPadaria.Context;
using CaixaPadaria.Services;
using CaixaPadaria.Views;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
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
            base.OnStartup(e);

            using (var context = new AppDbContext())
            {
                var dbInitializer = new DatabaseInitializer(context);
                dbInitializer.Initialize();
            }

            MainWindow = new LoginWindow();
            MainWindow.Show();
        }
    }

}
