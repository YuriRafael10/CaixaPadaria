using CaixaPadaria.Models;
using System.Collections.Generic;
using System.Windows;

namespace CaixaPadaria.Views.Windows
{
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
