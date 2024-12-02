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
            int quantity;
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

            if (!int.TryParse(QuantityTextBox.Text, out quantity) || quantity < 0)
            {
                MessageBox.Show("Quantidade inválida.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            decimal? costPrice = string.IsNullOrWhiteSpace(CostPriceTextBox.Text)
                ? null
                : decimal.Parse(CostPriceTextBox.Text); // Permitir nulo para custo

            if (BrandComboBox.SelectedValue == null || CategoryComboBox.SelectedValue == null)
            {
                MessageBox.Show("Selecione uma marca e uma categoria.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (var context = new AppDbContext())
                {
                    Product product = new Product
                    {
                        ProductId = barcode ?? 0, // Usar o valor inserido ou deixar como 0 para auto incremento
                        Name = name,
                        CostPrice = costPrice ?? 0, // Tratar nulo como 0
                        SalePrice = salePrice,
                        Quantity = quantity,
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
