using SistemaEscola.Utils;
using SistemaEscola.Model;
using SistemaEscola.View;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace SistemaEscola.ViewModel
{
	internal class MainViewModel : Observavel
	{
		private Model.SistemaEscola _se;
		private IDbCrud _bancoDeDados;

		public ObservableCollection<Pessoa> Pessoas { get { return _se.Pessoas; } }

		public ICommand AbrirFormularioAluno { get; set; }
		public ICommand AbrirFormularioProfessor{ get; set; }
		public ICommand AbrirFormularioFaxineiro { get; set; }

		public Pessoa PessoaSelecionada { get; set; }

		public ICommand RemoverPessoa { get; set; }
		public ICommand AtualizarPessoa { get; set; }

		public MainViewModel()
		{
			_se = new SistemaEscola.Model.SistemaEscola();
			_bancoDeDados = new PostgreSql("localhost", "postgres", "password", "escola", "5432");
			_bancoDeDados.CarregarDados(_se.Pessoas);

			AbrirFormularioAluno = new ExecutarComando((_) =>
			{
				FormularioAluno formulario = new FormularioAluno();
				Pessoa nova = new Aluno();

				formulario.DataContext = nova;
				formulario.ShowDialog();
				
				_se.AdicionarPessoa(nova);
				_bancoDeDados.Inserir(nova, typeof(Aluno));
			});

			AbrirFormularioProfessor = new ExecutarComando((_) =>
			{
				FormularioProfessor formulario = new FormularioProfessor();
				Pessoa nova = new Professor();

				formulario.DataContext = nova;
				formulario.ShowDialog();

				_se.AdicionarPessoa(nova);
				_bancoDeDados.Inserir(nova, typeof(Professor));
			});

			AbrirFormularioFaxineiro = new ExecutarComando((_) =>
			{
				FormularioFaxineiro formulario = new FormularioFaxineiro();
				Pessoa nova = new Faxineiro();

				formulario.DataContext = nova;
				formulario.ShowDialog();

				_se.AdicionarPessoa(nova);
				_bancoDeDados.Inserir(nova, typeof(Faxineiro));
			});

			RemoverPessoa = new ExecutarComando((_) =>
			{
				if (PessoaSelecionada != null)
				{
					PessoaSelecionada.SairDe(Pessoas);
					_bancoDeDados.Remover(PessoaSelecionada);
					PropriedadeMudou(nameof(Pessoas));
				}
			});

			AtualizarPessoa = new ExecutarComando((_) =>
			{
				if (PessoaSelecionada != null)
				{
					Aluno aluno = PessoaSelecionada as Aluno;
					Professor professor = PessoaSelecionada as Professor;
					Faxineiro faxineiro = PessoaSelecionada as Faxineiro;

					if (aluno != null)
					{
						Pessoa editado = new Aluno();
						FormularioAluno form = new FormularioAluno();
						form.DataContext = editado;
						form.ShowDialog();
						for (int i = 0; i < Pessoas.Count; ++i)
						{
							if (Pessoas[i] == PessoaSelecionada)
							{
								Pessoas.RemoveAt(i);
								Pessoas.Insert(i, editado);
							}
						}
					}

					if (professor != null)
					{
						Pessoa editado = new Professor();
						FormularioProfessor form = new FormularioProfessor();
						form.DataContext = editado;
						form.ShowDialog();
						for (int i = 0; i < Pessoas.Count; ++i)
						{
							if (Pessoas[i] == PessoaSelecionada)
							{
								Pessoas.RemoveAt(i);
								Pessoas.Insert(i, editado);
							}
						}
					}

					if (faxineiro != null)
					{
						Pessoa editado = new Faxineiro();
						FormularioFaxineiro form = new FormularioFaxineiro();
						form.DataContext = editado;
						form.ShowDialog();
						for (int i = 0; i < Pessoas.Count; ++i)
						{
							if (Pessoas[i] == PessoaSelecionada)
							{
								Pessoas.RemoveAt(i);
								Pessoas.Insert(i, editado);
							}
						}
					}
				}
			});
		}


	}
}
