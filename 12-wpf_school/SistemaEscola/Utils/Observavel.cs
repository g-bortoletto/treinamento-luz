using System.ComponentModel;

namespace SistemaEscola.Utils
{
	public class Observavel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public void PropriedadeMudou(string propriedade)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propriedade));
		}
	}
}
