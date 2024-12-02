using CaixaPadaria.Context;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CaixaPadaria.Views.UserControls.Products.Register
{
    /// <summary>
    /// Interação lógica para RegisterProductControl.xam
    /// </summary>
    public partial class RegisterProductControl : UserControl
    {
        public RegisterProductControl()
        {
            InitializeComponent();
            LoadBrandsAndCategories();
        }
        private void LoadBrandsAndCategories()
        {
            using (var context = new AppDbContext())
            {
                BrandComboBox.ItemsSource = context.Brands.ToList();
                BrandComboBox.DisplayMemberPath = "Name";
                BrandComboBox.SelectedValuePath = "BrandId";

                CategoryComboBox.ItemsSource = context.Categories.ToList();
                CategoryComboBox.DisplayMemberPath = "Name";
                CategoryComboBox.SelectedValuePath = "CategoryId";
            }
        }

        private void SaveProduct_Click(object sender, RoutedEventArgs e)
        {
            string name = ProductNameTextBox.Text.Trim();
            decimal salePrice;
            long? barcode = string.IsNullOrWhiteSpace(BarcodeTextBox.Text) ? null : long.Parse(BarcodeTextBox.Text);

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("O nome do produto é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!decimal.TryParse(SalePriceTextBox.Text, out salePrice) || salePrice < 0)
            {
                MessageBox.Show("Preço de venda inválido.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int? quantity = string.IsNullOrWhiteSpace(CostPriceTextBox.Text)
                ? null
                : int.Parse(CostPriceTextBox.Text);

            decimal? costPrice = string.IsNullOrWhiteSpace(CostPriceTextBox.Text)
                ? null
                : decimal.Parse(CostPriceTextBox.Text);

            if (BrandComboBox.SelectedValue == null || CategoryComboBox.SelectedValue == null)
            {
                MessageBox.Show("Selecione uma marca e uma categoria.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (var context = new AppDbContext())
                {
                    // Carregar todos os produtos no contexto
                    var existingProductNames = context.Products.Select(p => p.Name.ToLower()).ToList();

                    // Verificar se o nome já existe, ignorando maiúsculas/minúsculas
                    if (existingProductNames.Contains(name.ToLower()))
                    {
                        MessageBox.Show("Já existe um produto cadastrado com esse nome.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    // Verificar se o código de barras foi informado e se já existe
                    if (barcode.HasValue && context.Products.Any(p => p.ProductId == barcode.Value))
                    {
                        MessageBox.Show("O código de barras já está cadastrado para outro produto.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    // Caso o código de barras não tenha sido informado, buscar o menor código disponível
                    if (!barcode.HasValue)
                    {
                        // Carregar todos os IDs de produtos existentes
                        var existingIds = context.Products.Select(p => p.ProductId).OrderBy(p => p).ToList();

                        // Encontrar o menor número disponível (vago) no intervalo
                        long newBarcode = 1;
                        foreach (var id in existingIds)
                        {
                            if (id == newBarcode)
                            {
                                newBarcode++; // Se o ID atual for o esperado, incrementa
                            }
                            else
                            {
                                break; // O primeiro ID faltante é encontrado
                            }
                        }

                        barcode = newBarcode; // Atribui o primeiro código disponível
                    }

                    Product product = new Product
                    {
                        ProductId = barcode.Value, // Usar o valor gerado ou o informado
                        Name = name,
                        CostPrice = costPrice ?? 0, // Tratar nulo como 0
                        SalePrice = salePrice,
                        Quantity = quantity ?? 0,
                        BrandId = (int)BrandComboBox.SelectedValue,
                        CategoryId = (int)CategoryComboBox.SelectedValue
                    };

                    context.Products.Add(product);
                    context.SaveChanges();
                }

                MessageBox.Show("Produto cadastrado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar o produto: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            ProductNameTextBox.Text = string.Empty;
            CostPriceTextBox.Text = string.Empty;
            SalePriceTextBox.Text = string.Empty;
            QuantityTextBox.Text = string.Empty;
            BarcodeTextBox.Text = string.Empty;
            BrandComboBox.SelectedItem = null;
            CategoryComboBox.SelectedItem = null;
        }
    }
}
