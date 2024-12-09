using CaixaPadaria.Context;
using CaixaPadaria.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CaixaPadaria.Views.UserControls.Products.Edit
{
    /// <summary>
    /// Interação lógica para EditProductControl.xam
    /// </summary>
    public partial class EditProductControl : UserControl
    {
        public EditProductControl()
        {
            InitializeComponent();
            LoadBrands();
            LoadCategories();
            BarcodeTextBox.IsEnabled = false;
        }

        private void LoadBrands()
        {
            using (var context = new AppDbContext())
            {
                BrandComboBox.ItemsSource = context.Brands.ToList();
                BrandComboBox.DisplayMemberPath = "Name";
                BrandComboBox.SelectedValuePath = "BrandId";
            }
        }

        private void LoadCategories()
        {
            using (var context = new AppDbContext())
            {
                CategoryComboBox.ItemsSource = context.Categories.ToList();
                CategoryComboBox.DisplayMemberPath = "Name";
                CategoryComboBox.SelectedValuePath = "CategoryId";
            }
        }

        private void SearchProduct_Click(object sender, RoutedEventArgs e)
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
                    BarcodeTextBox.Text = selectedProduct.ProductId.ToString();
                    ProductNameTextBox.Text = selectedProduct.Name;
                    CostPriceTextBox.Text = selectedProduct.CostPrice?.ToString() ?? "0";
                    SalePriceTextBox.Text = selectedProduct.SalePrice.ToString();
                    QuantityTextBox.Text = selectedProduct.Quantity.ToString();
                    BrandComboBox.SelectedValue = selectedProduct.BrandId;
                    CategoryComboBox.SelectedValue = selectedProduct.CategoryId;

                    BarcodeTextBox.IsEnabled = false;
                }
            }

        }

        private void SearchTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                SearchProduct_Click(sender, e);
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearFields_Click(sender, e);
        }

        private void SaveProduct_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new AppDbContext())
            {
                // Garantir que o BarcodeTextBox não esteja vazio
                if (string.IsNullOrWhiteSpace(BarcodeTextBox.Text))
                {
                    MessageBox.Show("O código de barras é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Converter o código de barras (ID) para long, mas sem permitir alteração
                if (!long.TryParse(BarcodeTextBox.Text, out long productId))
                {
                    MessageBox.Show("O código de barras informado é inválido.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Buscar o produto no banco de dados
                var product = context.Products.FirstOrDefault(p => p.ProductId == productId);

                if (product != null)
                {
                    // Verificar se a marca e a categoria foram selecionadas
                    if (BrandComboBox.SelectedValue == null)
                    {
                        MessageBox.Show("Por favor, selecione uma marca.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    if (CategoryComboBox.SelectedValue == null)
                    {
                        MessageBox.Show("Por favor, selecione uma categoria.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    product.Name = ProductNameTextBox.Text.Trim();
                    product.CostPrice = decimal.TryParse(CostPriceTextBox.Text, out var costPrice) ? costPrice : 0;
                    product.SalePrice = decimal.TryParse(SalePriceTextBox.Text, out var salePrice) ? salePrice : 0;
                    product.Quantity = int.TryParse(QuantityTextBox.Text, out var quantity) ? quantity : 0;
                    product.BrandId = (int)BrandComboBox.SelectedValue;
                    product.CategoryId = (int)CategoryComboBox.SelectedValue;

                    context.SaveChanges();
                    MessageBox.Show("Produto atualizado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearFields_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Erro ao salvar o produto. Verifique os dados.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ClearFields_Click(object sender, RoutedEventArgs e)
        {
            BarcodeTextBox.Clear();
            ProductNameTextBox.Clear();
            CostPriceTextBox.Clear();
            SalePriceTextBox.Clear();
            QuantityTextBox.Clear();
            SearchTextBox.Clear();
            BrandComboBox.SelectedItem = null;
            CategoryComboBox.SelectedItem = null;
        }
    }
}
