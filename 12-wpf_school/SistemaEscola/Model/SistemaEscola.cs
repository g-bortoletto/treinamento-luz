using System.Collections.ObjectModel;

namespace SistemaEscola.Model
{

	public class SistemaEscola
	{
		private ObservableCollection<Pessoa> _pessoas;
		public ObservableCollection<Pessoa> Pessoas { get { return _pessoas; } }

		public SistemaEscola()
		{
			_pessoas = new ObservableCollection<Pessoa>();
		}

		public void AdicionarPessoa(Pessoa pessoa)
		{
			if (pessoa != null &&
				!string.IsNullOrEmpty(pessoa.Nome) &&
				!string.IsNullOrEmpty(pessoa.Sobrenome))
			{
				pessoa.InserirEm(_pessoas);
			}
		}
		public void RemoverPessoa(Pessoa pessoa) => pessoa.SairDe(_pessoas);
	}

}
