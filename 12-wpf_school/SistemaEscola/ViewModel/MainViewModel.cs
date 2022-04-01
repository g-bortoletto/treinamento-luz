using SistemaEscola.Model;
using SistemaEscola.Utils;
using SistemaEscola.View;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SistemaEscola.ViewModel
{
    internal class MainViewModel : Observavel
    {
        private IDbCrud _bancoDeDados;
        private Model.SistemaEscola _se;

        public MainViewModel()
        {
            Init();

            AbrirFormularioAluno = new ExecutarComando(delegate { AdicionarAluno(); });

            AbrirFormularioProfessor = new ExecutarComando(delegate { AdicionarProfessor(); });

            AbrirFormularioFaxineiro = new ExecutarComando(delegate { AdicionarFaxineiro(); });

            ComandoRemoverPessoa = new ExecutarComando(delegate { RemoverPessoa(); });

            ComandoAtualizarPessoa = new ExecutarComando(delegate { AtualizarPessoa(); });
        }

        public ICommand AbrirFormularioAluno { get; set; }
        public ICommand AbrirFormularioFaxineiro { get; set; }
        public ICommand AbrirFormularioProfessor { get; set; }
        public ICommand ComandoAtualizarPessoa { get; set; }
        public ICommand ComandoRemoverPessoa { get; set; }
        public ObservableCollection<Pessoa> Pessoas
        { get { return _se.Pessoas; } }
        public Pessoa PessoaSelecionada { get; set; }

        private void AdicionarAluno()
        {
            FormularioAluno formulario = new FormularioAluno();
            Pessoa nova = new Aluno();

            formulario.DataContext = nova;
            formulario.ShowDialog();

            _se.AdicionarPessoa(nova as Aluno, _bancoDeDados);
        }

        private void AdicionarFaxineiro()
        {
            FormularioFaxineiro formulario = new FormularioFaxineiro();
            Pessoa nova = new Faxineiro();

            formulario.DataContext = nova;
            formulario.ShowDialog();

            _se.AdicionarPessoa(nova as Faxineiro, _bancoDeDados);
        }

        private void AdicionarProfessor()
        {
            FormularioProfessor formulario = new FormularioProfessor();
            Pessoa nova = new Professor();

            formulario.DataContext = nova;
            formulario.ShowDialog();

            _se.AdicionarPessoa(nova as Professor, _bancoDeDados);
        }

        private void AtualizarPessoa()
        {
            if (PessoaSelecionada == null)
            {
                return;
            }

            if (PessoaSelecionada is Aluno aluno)
            {
                Pessoa editado = new Aluno();
                FormularioAluno form = new FormularioAluno();
                form.DataContext = editado;
                form.ShowDialog();
                if (!string.IsNullOrEmpty(editado.Nome) &&
                    !string.IsNullOrEmpty(editado.Sobrenome))

                {
                    for (int i = 0; i < Pessoas.Count; ++i)
                    {
                        if (Pessoas[i] == PessoaSelecionada)
                        {
                            Pessoas.RemoveAt(i);
                            Pessoas.Insert(i, editado);
                        }
                    }
                    _bancoDeDados.Remover(aluno);
                    _bancoDeDados.Inserir(editado);
                }
            }

            if (PessoaSelecionada is Professor professor)
            {
                Pessoa editado = new Professor();
                FormularioProfessor form = new FormularioProfessor();
                form.DataContext = editado;
                form.ShowDialog();
                if (!string.IsNullOrEmpty(editado.Nome) &&
                    !string.IsNullOrEmpty(editado.Sobrenome))

                {
                    for (int i = 0; i < Pessoas.Count; ++i)
                    {
                        if (Pessoas[i] == PessoaSelecionada)
                        {
                            Pessoas.RemoveAt(i);
                            Pessoas.Insert(i, editado);
                        }
                    }
                    _bancoDeDados.Remover(professor);
                    _bancoDeDados.Inserir(editado);
                }
            }

            if (PessoaSelecionada is Faxineiro faxineiro)
            {
                Pessoa editado = new Faxineiro();
                FormularioFaxineiro form = new FormularioFaxineiro();
                form.DataContext = editado;
                form.ShowDialog();
                if (!string.IsNullOrEmpty(editado.Nome) &&
                    !string.IsNullOrEmpty(editado.Sobrenome))

                {
                    for (int i = 0; i < Pessoas.Count; ++i)
                    {
                        if (Pessoas[i] == PessoaSelecionada)
                        {
                            Pessoas.RemoveAt(i);
                            Pessoas.Insert(i, editado);
                        }
                    }
                    _bancoDeDados.Remover(faxineiro);
                    _bancoDeDados.Inserir(editado);
                }
            }
        }

        private void Init()
        {
            _se = new Model.SistemaEscola();
            //_bancoDeDados = new PostgreSql("localhost", "root", "example", "escola");
            _bancoDeDados = new MariaDb("localhost", "root", "example", "escola");
            _bancoDeDados.CarregarDados(_se.Pessoas);
        }

        private void RemoverPessoa()
        {
            if (PessoaSelecionada != null)
            {
                _bancoDeDados.Remover(PessoaSelecionada);
                PessoaSelecionada.SairDe(Pessoas);
                PropriedadeMudou(nameof(Pessoas));
            }
        }
    }
}