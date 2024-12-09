using CaixaPadaria.Models;
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
    /// Lógica interna para ProductSearchWindow.xaml
    /// </summary>
    public partial class ProductSearchWindow : Window
    {
        public Product SelectedProduct { get; private set; }

        public ProductSearchWindow(List<Product> products)
        {
            InitializeComponent();
            ProductDataGrid.ItemsSource = products;
        }

        private void SelectProduct_Click(object sender, RoutedEventArgs e)
        {
            SelectedProduct = ProductDataGrid.SelectedItem as Product;
            if (SelectedProduct != null)
            {
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Por favor, selecione um produto.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
