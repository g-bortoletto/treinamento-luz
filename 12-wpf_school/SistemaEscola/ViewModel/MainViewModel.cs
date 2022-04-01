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

        private void AtualizarAluno()
        {
            Aluno editado = PessoaSelecionada as Aluno;
            FormularioAluno form = new FormularioAluno();
            form.DataContext = editado;
            form.ShowDialog();
            if (!string.IsNullOrEmpty(editado.Nome) &&
                !string.IsNullOrEmpty(editado.Sobrenome))

            {
                foreach (Pessoa pessoa in Pessoas)
                {
                    if (pessoa.Id == editado.Id)
                    {
                        pessoa.Nome = editado.Nome;
                        pessoa.Sobrenome = editado.Sobrenome;
                        pessoa.DataNascimento = editado.DataNascimento;
                        (pessoa as Aluno).Matricula = editado.Matricula;
                    }
                }
                _bancoDeDados.Editar(PessoaSelecionada as Aluno, editado);
            }
        }

        private void AtualizarFaxineiro()
        {
            Faxineiro editado = PessoaSelecionada as Faxineiro;
            FormularioFaxineiro form = new FormularioFaxineiro();
            form.DataContext = editado;
            form.ShowDialog();
            if (!string.IsNullOrEmpty(editado.Nome) &&
                !string.IsNullOrEmpty(editado.Sobrenome))

            {
                foreach (Pessoa pessoa in Pessoas)
                {
                    if (pessoa.Id == editado.Id)
                    {
                        pessoa.Nome = editado.Nome;
                        pessoa.Sobrenome = editado.Sobrenome;
                        pessoa.DataNascimento = editado.DataNascimento;
                        (pessoa as Faxineiro).Salario = editado.Salario;
                    }
                }
                _bancoDeDados.Editar(PessoaSelecionada as Faxineiro, editado);
            }
        }

        private void AtualizarPessoa()
        {
            if (PessoaSelecionada == null) { return; }
            if (PessoaSelecionada is Aluno) { AtualizarAluno(); }
            if (PessoaSelecionada is Professor) { AtualizarProfessor(); }
            if (PessoaSelecionada is Faxineiro) { AtualizarFaxineiro(); }
        }

        private void AtualizarProfessor()
        {
            Professor editado = PessoaSelecionada as Professor;
            FormularioProfessor form = new FormularioProfessor();
            form.DataContext = editado;
            form.ShowDialog();
            if (!string.IsNullOrEmpty(editado.Nome) &&
                !string.IsNullOrEmpty(editado.Sobrenome))

            {
                foreach (Pessoa pessoa in Pessoas)
                {
                    if (pessoa.Id == editado.Id)
                    {
                        pessoa.Nome = editado.Nome;
                        pessoa.Sobrenome = editado.Sobrenome;
                        pessoa.DataNascimento = editado.DataNascimento;
                        (pessoa as Professor).Salario = editado.Salario;
                        (pessoa as Professor).Disciplina = editado.Disciplina;
                    }
                }
                _bancoDeDados.Editar(PessoaSelecionada as Professor, editado);
            }
        }
        private void Init()
        {
            _se = new Model.SistemaEscola();
            _bancoDeDados = new PostgreSql("localhost", "root", "example", "escola");
            //_bancoDeDados = new MariaDb("localhost", "root", "example", "escola");
            _bancoDeDados.CarregarDados(_se.Pessoas);
        }

        private void RemoverPessoa()
        {
            if (PessoaSelecionada != null)
            {
                PessoaSelecionada.SairDe(Pessoas, _bancoDeDados);
                PropriedadeMudou(nameof(Pessoas));
            }
        }
    }
}