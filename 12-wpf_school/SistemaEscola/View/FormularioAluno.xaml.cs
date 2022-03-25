using System.Windows;

namespace SistemaEscola.View
{
	/// <summary>
	/// Interaction logic for Formulario.xaml
	/// </summary>
	public partial class FormularioAluno : Window
	{
		public FormularioAluno()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
		}
	}
}
