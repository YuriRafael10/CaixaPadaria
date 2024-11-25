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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CaixaPadaria.Views.UserControls.Products.Register
{
    /// <summary>
    /// Interação lógica para RegisterCategoryControl.xam
    /// </summary>
    public partial class RegisterCategoryControl : UserControl
    {
        public RegisterCategoryControl()
        {
            InitializeComponent();
        }

        private void SaveCategory_Click(object sender, RoutedEventArgs e)
        {
            string categoryName = CategoryNameTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(categoryName))
            {
                MessageBox.Show("O nome da categoria é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (categoryName.Length > 50)
            {
                MessageBox.Show("O nome da categoria deve ter no máximo 50 caracteres.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                Category newCategory = new Category
                {
                    Name = categoryName,
                    TotalSales = 0
                };


                SaveBrandToDatabase(newCategory);

                MessageBox.Show($"Categoria '{newCategory.Name}' registrada com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

                CategoryNameTextBox.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar a categoria: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveBrandToDatabase(Category category)
        {
            using (var context = new AppDbContext())
            {
                if (context.Categories.Any(b => b.Name == category.Name))
                {
                    throw new InvalidOperationException("Já existe uma marca com este nome.");
                }

                context.Categories.Add(category);

                context.SaveChanges();
            }
        }
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            CategoryNameTextBox.Text = string.Empty;
        }
    }

}
