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

namespace CaixaPadaria.Views.UserControls.Products
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

        // Função para salvar a marca
        private void SaveBrandButton_Click(object sender, RoutedEventArgs e)
        {
            string brandName = BrandNameTextBox.Text;

            if (string.IsNullOrWhiteSpace(brandName))
            {
                MessageBox.Show("Por favor, insira um nome para a marca.", "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Cria a nova marca
            Brand newBrand = new Brand
            {
                Name = brandName,
                TotalSales = 0 // Inicializa com 0, pois será atualizado com vendas futuras
            };

            // Aqui você pode adicionar a lógica para salvar a marca no banco de dados

            MessageBox.Show("Marca registrada com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
            ClearForm();
        }

        // Função para limpar o formulário
        private void ClearFormButton_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }

        // Método auxiliar para limpar o formulário
        private void ClearForm()
        {
            BrandNameTextBox.Text = string.Empty;
            TotalSalesTextBox.Text = "0.00";
        }
    }
}
