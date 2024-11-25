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
    /// Interação lógica para RegisterBrandControl.xam
    /// </summary>
    public partial class RegisterBrandControl : UserControl
    {
        public RegisterBrandControl()
        {
            InitializeComponent();
        }

        private void SaveBrand_Click(object sender, RoutedEventArgs e)
        {
            string brandName = BrandNameTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(brandName))
            {
                MessageBox.Show("O nome da marca é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (brandName.Length > 50)
            {
                MessageBox.Show("O nome da marca deve ter no máximo 50 caracteres.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                Brand newBrand = new Brand
                {
                    Name = brandName,
                    TotalSales = 0
                };


                SaveBrandToDatabase(newBrand);

                MessageBox.Show($"Marca '{newBrand.Name}' registrada com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

                BrandNameTextBox.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar a marca: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveBrandToDatabase(Brand brand)
        {
            using (var context = new AppDbContext())
            {
                if (context.Brands.Any(b => b.Name == brand.Name))
                {
                    throw new InvalidOperationException("Já existe uma marca com este nome.");
                }

                context.Brands.Add(brand);

                context.SaveChanges();
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            BrandNameTextBox.Text = string.Empty;
        }
    }
}
