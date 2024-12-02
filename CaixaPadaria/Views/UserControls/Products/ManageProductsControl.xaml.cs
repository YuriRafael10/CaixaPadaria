using CaixaPadaria.Views.UserControls.Products;
using CaixaPadaria.Views.UserControls.Products.Register;
using CaixaPadaria.Views.UserControls.Products.Edit;
using CaixaPadaria.Views.UserControls.Products.Delete;
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

namespace CaixaPadaria.Views.UserControls
{
    /// <summary>
    /// Interação lógica para ManageProductsControl.xam
    /// </summary>
    public partial class ManageProductsControl : UserControl
    {
        public ManageProductsControl()
        {
            InitializeComponent();
        }
        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new RegisterProductControl();
        }

        private void EditProductButton_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new EditProductControl();
        }

        private void DeleteProductButton_Click(object sender, RoutedEventArgs e)
        {
            // Lógica para excluir um produto
        }

        private void AddBrandButton_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new RegisterBrandControl();
        }

        private void EditBrandButton_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new EditBrandControl();
        }

        private void DeleteBrandButton_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new DeleteBrandControl();
        }

        private void AddCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new RegisterCategoryControl();
        }

        private void EditCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new EditCategoryControl();
        }

        private void DeleteCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new DeleteCategoryControl();
        }
    }
}