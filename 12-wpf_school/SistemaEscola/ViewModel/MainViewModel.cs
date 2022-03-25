using SistemaEscola.Utils;
using SistemaEscola.Model;
using SistemaEscola.View;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace SistemaEscola.ViewModel
{
	internal class MainViewModel : Observavel, IDbCrud
	{
		private SistemaEscola.Model.SistemaEscola _se;

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

			AbrirFormularioAluno = new ExecutarComando((_) =>
			{
				FormularioAluno formulario = new FormularioAluno();
				Pessoa nova = new Aluno();

				formulario.DataContext = nova;
				formulario.ShowDialog();
				
				_se.AdicionarPessoa(nova);
			});

			AbrirFormularioProfessor = new ExecutarComando((_) =>
			{
				FormularioProfessor formulario = new FormularioProfessor();
				Pessoa nova = new Professor();

				formulario.DataContext = nova;
				formulario.ShowDialog();

				_se.AdicionarPessoa(nova);
			});

			AbrirFormularioFaxineiro = new ExecutarComando((_) =>
			{
				FormularioFaxineiro formulario = new FormularioFaxineiro();
				Pessoa nova = new Faxineiro();

				formulario.DataContext = nova;
				formulario.ShowDialog();

				_se.AdicionarPessoa(nova);
			});

			RemoverPessoa = new ExecutarComando((_) =>
			{
				if (PessoaSelecionada != null)
				{
					PessoaSelecionada.SairDe(Pessoas);
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
