using CaixaPadaria.Views.UserControls;
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
