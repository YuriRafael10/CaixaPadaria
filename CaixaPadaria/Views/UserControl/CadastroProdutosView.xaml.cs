using System.Windows;
using System.Windows.Controls;

namespace CaixaPadaria.Views
{
    public partial class CadastroProdutosView : UserControl
    {
        public CadastroProdutosView()
        {
            InitializeComponent();
        }

        private void Salvar_Click(object sender, RoutedEventArgs e)
        {
            // Exemplo básico de como capturar os valores inseridos
            string nomeProduto = NomeProdutoTextBox.Text;
            string descricao = DescricaoTextBox.Text;
            string preco = PrecoTextBox.Text;
            string quantidade = QuantidadeTextBox.Text;

            // Validação e lógica de salvar o produto
            MessageBox.Show("Produto salvo com sucesso!");
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            // Limpa os campos ou fecha a tela, dependendo da funcionalidade desejada
            NomeProdutoTextBox.Clear();
            DescricaoTextBox.Clear();
            PrecoTextBox.Clear();
            QuantidadeTextBox.Clear();
        }
    }
}
