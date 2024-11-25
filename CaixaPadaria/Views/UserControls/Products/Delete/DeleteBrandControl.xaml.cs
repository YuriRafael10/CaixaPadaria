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

namespace CaixaPadaria.Views.UserControls.Products.Delete
{
    /// <summary>
    /// Interação lógica para DeleteBrandControl.xam
    /// </summary>
    public partial class DeleteBrandControl : UserControl
    {
        public DeleteBrandControl()
        {
            InitializeComponent();
            LoadBrands();
        }

        private void LoadBrands()
        {
            using (var context = new AppDbContext())
            {
                var brands = context.Brands.ToList();
                BrandComboBox.ItemsSource = brands;
                BrandComboBox.DisplayMemberPath = "Name"; // Exibir o nome no ComboBox
                BrandComboBox.SelectedValuePath = "BrandId"; // Usar BrandId como valor
            }
        }

        private void DeleteBrand_Click(object sender, RoutedEventArgs e)
        {
            if (BrandComboBox.SelectedItem is Brand selectedBrand)
            {
                var result = MessageBox.Show(
                    $"Tem certeza que deseja apagar a marca '{selectedBrand.Name}'?",
                    "Confirmação",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        using (var context = new AppDbContext())
                        {
                            var brand = context.Brands.FirstOrDefault(b => b.BrandId == selectedBrand.BrandId);
                            if (brand != null)
                            {
                                // Remover a marca
                                context.Brands.Remove(brand);
                                context.SaveChanges();
                            }
                        }

                        MessageBox.Show("Marca apagada com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Recarregar lista de marcas
                        LoadBrands();
                        BrandComboBox.SelectedItem = null;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao apagar a marca: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione uma marca para apagar.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            // Limpar seleção
            BrandComboBox.SelectedItem = null;
        }
    }
}
