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
    /// Interação lógica para EditCategoryControl.xam
    /// </summary>
    public partial class EditCategoryControl : UserControl
    {
        public EditCategoryControl()
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

        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CategoryComboBox.SelectedItem is Category selectedCategory)
            {
                NewCategoryNameTextBox.Text = selectedCategory.Name;
            }
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            if (CategoryComboBox.SelectedItem is Category selectedCategory)
            {
                string newName = NewCategoryNameTextBox.Text.Trim();

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
                        var category = context.Categories.FirstOrDefault(b => b.CategoryId == b.CategoryId);
                        if (category != null)
                        {
                            category.Name = newName;
                            context.SaveChanges();
                        }
                    }

                    MessageBox.Show("Categoria atualizada com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

                    LoadCategories();
                    CategoryComboBox.SelectedItem = null;
                    NewCategoryNameTextBox.Text = string.Empty;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao atualizar a categoria: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            CategoryComboBox.SelectedItem = null;
            NewCategoryNameTextBox.Text = string.Empty;
        }
    }
}

