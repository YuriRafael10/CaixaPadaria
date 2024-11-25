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
    /// Interação lógica para DeleteCategoryControl.xam
    /// </summary>
    public partial class DeleteCategoryControl : UserControl
    {
        public DeleteCategoryControl()
        {
            InitializeComponent();
            LoadCategories();
        }

        private void LoadCategories()
        {
            using (var context = new AppDbContext())
            {
                var categories = context.Categories.ToList();
                CategoryComboBox.ItemsSource = categories;
                CategoryComboBox.DisplayMemberPath = "Name";
                CategoryComboBox.SelectedValuePath = "CategoryId";
            }
        }

        private void DeleteCategory_Click(object sender, RoutedEventArgs e)
        {
            if (CategoryComboBox.SelectedItem is Category selectedCategory)
            {
                var result = MessageBox.Show(
                    $"Tem certeza que deseja apagar a categoria '{selectedCategory.Name}'?",
                    "Confirmação",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        using (var context = new AppDbContext())
                        {
                            var category = context.Categories.FirstOrDefault(b => b.CategoryId == selectedCategory.CategoryId);
                            if (category != null)
                            {
                                // Remover a marca
                                context.Categories.Remove(category);
                                context.SaveChanges();
                            }
                        }

                        MessageBox.Show("Categoria apagada com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Recarregar lista de marcas
                        LoadCategories();
                        CategoryComboBox.SelectedItem = null;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao apagar a categoria: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione uma categoria para apagar.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            // Limpar seleção
            CategoryComboBox.SelectedItem = null;
        }
    }
}
