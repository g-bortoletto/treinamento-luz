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

		public void AdicionarPessoa(Pessoa pessoa, Utils.IDbCrud bd = null)
		{
			if (pessoa != null &&
				!string.IsNullOrEmpty(pessoa.Nome) &&
				!string.IsNullOrEmpty(pessoa.Sobrenome))
			{
				pessoa.InserirEm(_pessoas, bd);
			}
		}
		public void RemoverPessoa(Pessoa pessoa, Utils.IDbCrud bd = null) 
		{
			pessoa.SairDe(_pessoas, bd); 
		}
	}

}
