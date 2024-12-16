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

namespace CaixaPadaria.Views.UserControls.Users.Register
{
    /// <summary>
    /// Interação lógica para RegisterUserControl.xam
    /// </summary>
    public partial class RegisterUserControl : UserControl
    {
        public RegisterUserControl()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new AppDbContext())
            {
                if (string.IsNullOrWhiteSpace(NameTextBox.Text) || string.IsNullOrWhiteSpace(PasswordTextBox.Password))
                {
                    MessageBox.Show("Por favor, preencha todos os campos.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var newUser = new User
                {
                    Name = NameTextBox.Text.Trim(),
                    Password = PasswordTextBox.Password,
                    IsAdmin = IsAdminCheckBox.IsChecked ?? false
                };

                context.Users.Add(newUser);
                context.SaveChanges();

                MessageBox.Show("Usuário registrado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

                ClearFields();
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }
        private void ClearFields()
        {
            NameTextBox.Clear();
            PasswordTextBox.Clear();
            IsAdminCheckBox.IsChecked = false;
        }
    }
}
