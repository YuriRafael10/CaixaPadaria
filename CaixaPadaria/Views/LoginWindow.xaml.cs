using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CaixaPadaria.Context;
using CaixaPadaria.Models;

namespace CaixaPadaria.Views
{
    /// <summary>
    /// Lógica interna para LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            LoginBtn.Click += SubmitClick;
        }

        private void SubmitClick(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            using (var context = new AppDbContext())
            {
                var user = context.Users
                    .Where(u => u.Name == username && u.Password == password)
                    .FirstOrDefault();

                if (user != null)
                {
                    GrantAccess();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Usuário ou senha incorretos.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void SearchTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                SubmitClick(sender, e);
            }
        }

        public void GrantAccess()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
