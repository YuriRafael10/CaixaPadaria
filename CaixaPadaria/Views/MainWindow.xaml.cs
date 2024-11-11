using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CaixaPadaria.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ConfirmBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SettingsIcon_Click(object sender, MouseButtonEventArgs e)
        {
            // Instancia e exibe a nova janela
            ConfigWindow configWindow = new ConfigWindow();
            configWindow.ShowDialog(); // Usar ShowDialog para janela modal ou Show para janela não-modal
        }

    }
}