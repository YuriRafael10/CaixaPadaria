using CaixaPadaria.Views.UserControls;
using System.Windows;

namespace CaixaPadaria.Views
{
    /// <summary>
    /// Lógica interna para ConfigWindow.xaml
    /// </summary>
    public partial class ConfigWindow : Window
    {
        public ConfigWindow()
        {
            InitializeComponent();
        }

        private void GerenciarContas_Click(object sender, RoutedEventArgs e)
        {

        }
        private void ManageProductsButton_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Carrega o UserControl ManageProductsControl no MainContent
            MainContent.Content = new ManageProductsControl();
        }
    }
}
