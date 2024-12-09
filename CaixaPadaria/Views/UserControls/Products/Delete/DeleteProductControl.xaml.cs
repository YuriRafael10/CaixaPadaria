using CaixaPadaria.Context;
using CaixaPadaria.Models;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CaixaPadaria.Views.UserControls.Products.Delete
{
    public partial class DeleteProductControl : UserControl
    {
        public DeleteProductControl()
        {
            InitializeComponent();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new AppDbContext())
            {
                string searchTerm = SearchTextBox.Text.Trim();
                var products = context.Products.Where(p => p.ProductId.ToString() == searchTerm || p.Name.Contains(searchTerm)).ToList();

                if (products == null)
                {
                    MessageBox.Show("Produto não encontrado.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    OpenProductSearchWindow(products);
                }
            }
        }

        private void OpenProductSearchWindow(IEnumerable<Product> products)
        {
            var searchWindow = new ProductSearchWindow(products.ToList());
            if (searchWindow.ShowDialog() == true)
            {
                var selectedProduct = searchWindow.SelectedProduct;

                if (selectedProduct != null)
                {
                    ProductDetailsPanel.Visibility = Visibility.Visible;

                    ProductCodeText.Text = $"Código: {selectedProduct.ProductId}";
                    ProductNameText.Text = $"Nome: {selectedProduct.Name}";
                    ProductSalePriceText.Text = $"Preço de Venda: {selectedProduct.SalePrice:C}";
                    ProductQuantityText.Text = $"Quantidade: {selectedProduct.Quantity}";
                }
            }
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (ProductDetailsPanel.Visibility == Visibility.Collapsed)
            {
                MessageBox.Show("Por favor, selecione um produto para apagar.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var result = MessageBox.Show(
                $"Tem certeza de que deseja excluir o produto '{ProductNameText.Text}'? Esta ação não pode ser desfeita.",
                "Confirmação",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                using (var context = new AppDbContext())
                {
                    var productId = long.Parse(ProductCodeText.Text.Split(':')[1].Trim());
                    var product = context.Products.FirstOrDefault(p => p.ProductId == productId);

                    if (product != null)
                    {
                        context.Products.Remove(product);
                        context.SaveChanges();
                    }
                }

                MessageBox.Show("Produto excluído com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                ClearFields();
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            SearchTextBox.Clear();
            ProductDetailsPanel.Visibility = Visibility.Collapsed;
        }
    }
}
