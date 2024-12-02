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
            BarcodeTextBox.IsEnabled = false; // Desabilitar o campo de código de barras no início
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
                var product = context.Products
                    .FirstOrDefault(p => p.ProductId.ToString() == searchTerm || p.Name.Contains(searchTerm));

                if (product != null)
                {
                    BarcodeTextBox.Text = product.ProductId.ToString();
                    ProductNameTextBox.Text = product.Name;
                    CostPriceTextBox.Text = product.CostPrice?.ToString() ?? "0";
                    SalePriceTextBox.Text = product.SalePrice.ToString();
                    QuantityTextBox.Text = product.Quantity.ToString();
                    BrandComboBox.SelectedValue = product.BrandId;
                    CategoryComboBox.SelectedValue = product.CategoryId;

                    // Desabilitar o campo do código de barras para não permitir alteração
                    BarcodeTextBox.IsEnabled = false;
                }
                else
                {
                    MessageBox.Show("Produto não encontrado.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
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

                    // Atualizar os campos do produto, exceto o ID
                    product.Name = ProductNameTextBox.Text.Trim();
                    product.CostPrice = decimal.TryParse(CostPriceTextBox.Text, out var costPrice) ? costPrice : 0;
                    product.SalePrice = decimal.TryParse(SalePriceTextBox.Text, out var salePrice) ? salePrice : 0;
                    product.Quantity = int.TryParse(QuantityTextBox.Text, out var quantity) ? quantity : 0;
                    product.BrandId = (int)BrandComboBox.SelectedValue;
                    product.CategoryId = (int)CategoryComboBox.SelectedValue;

                    // Salvar as alterações no banco de dados
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
            // Limpar campos e reabilitar o campo do código de barras
            BarcodeTextBox.Clear();
            ProductNameTextBox.Clear();
            CostPriceTextBox.Clear();
            SalePriceTextBox.Clear();
            QuantityTextBox.Clear();
            BrandComboBox.SelectedItem = null;
            CategoryComboBox.SelectedItem = null;

            // Reabilitar o campo do código de barras para permitir busca novamente
            BarcodeTextBox.IsEnabled = false; // Desabilitar o campo de código de barras após limpar
        }
    }
}
