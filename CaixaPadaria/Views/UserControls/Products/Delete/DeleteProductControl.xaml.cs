using CaixaPadaria.Context;
using CaixaPadaria.Models;
using CaixaPadaria.Views.Windows;
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

        private void SearchByCode_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SearchByCodeTextBox.Text) ||
                !long.TryParse(SearchByCodeTextBox.Text, out long code))
            {
                MessageBox.Show("Por favor, insira um código válido.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (var context = new AppDbContext())
            {
                var product = context.Products.FirstOrDefault(p => p.ProductId == code);

                if (product == null)
                {
                    MessageBox.Show("Produto não encontrado.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    OpenProductSearchWindow(new[] { product });
                }
            }
        }

        private void SearchByName_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = SearchByNameTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                MessageBox.Show("Por favor, insira um nome válido.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (var context = new AppDbContext())
            {
                var products = context.Products
                    .Where(p => p.Name.Contains(searchTerm))
                    .ToList();

                if (products.Count == 0)
                {
                    MessageBox.Show("Nenhum produto encontrado.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
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
            SearchByCodeTextBox.Clear();
            SearchByNameTextBox.Clear();
            ProductDetailsPanel.Visibility = Visibility.Collapsed;
        }
    }
}
