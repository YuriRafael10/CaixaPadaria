using CaixaPadaria.Views.UserControls.Products.Register;
using CaixaPadaria.Views.UserControls.Users.Register;
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

namespace CaixaPadaria.Views.UserControls.Users
{
    /// <summary>
    /// Interação lógica para ManageUsersControl.xam
    /// </summary>
    public partial class ManageUsersControl : UserControl
    {
        public ManageUsersControl()
        {
            InitializeComponent();
        }

        private void AddEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new RegisterUserControl();
        }

        private void EditEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void DeleteEmployeeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddClientButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditClientButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteClientButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
