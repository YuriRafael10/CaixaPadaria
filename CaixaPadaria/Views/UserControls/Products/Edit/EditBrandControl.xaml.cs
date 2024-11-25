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

namespace CaixaPadaria.Views.UserControls.Products.Edit
{
    /// <summary>
    /// Interação lógica para EditBrandControl.xam
    /// </summary>
    public partial class EditBrandControl : UserControl
    {
        public EditBrandControl()
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
                BrandComboBox.DisplayMemberPath = "Name";
                BrandComboBox.SelectedValuePath = "BrandId";
            }
        }

        private void BrandComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BrandComboBox.SelectedItem is Brand selectedBrand)
            {
                NewBrandNameTextBox.Text = selectedBrand.Name;
            }
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            if (BrandComboBox.SelectedItem is Brand selectedBrand)
            {
                string newName = NewBrandNameTextBox.Text.Trim();

                if (string.IsNullOrWhiteSpace(newName))
                {
                    MessageBox.Show("O novo nome é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (newName.Length > 20)
                {
                    MessageBox.Show("O nome deve ter no máximo 20 caracteres.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                try
                {
                    using (var context = new AppDbContext())
                    {
                        var brand = context.Brands.FirstOrDefault(b => b.BrandId == selectedBrand.BrandId);
                        if (brand != null)
                        {
                            brand.Name = newName;
                            context.SaveChanges();
                        }
                    }

                    MessageBox.Show("Marca atualizada com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

                    LoadBrands();
                    BrandComboBox.SelectedItem = null;
                    NewBrandNameTextBox.Text = string.Empty;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao atualizar a marca: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            // Limpar seleção e campo de texto
            BrandComboBox.SelectedItem = null;
            NewBrandNameTextBox.Text = string.Empty;
        }
    }
}
