using Npgsql;
using NpgsqlTypes;
using SistemaEscola.Model;
using System.Collections.ObjectModel;

namespace SistemaEscola.Utils
{
    public class PostgreSql : IDbCrud
    {
        private readonly NpgsqlConnection _conexao;

        public PostgreSql(string host, string usuario, string senha, string bancoDeDados)
        {
            var str = new NpgsqlConnectionStringBuilder
            {
                Host = host,
                Username = usuario,
                Password = senha,
                Database = bancoDeDados,
                Port = 5432
            };

            _conexao = new NpgsqlConnection(str.ConnectionString);

            Conectar();
        }

        public void CarregarDados(Collection<Pessoa> list)
        {
            CarregarAlunos(list);
            CarregarFaxineiros(list);
            CarregarProfessores(list);
        }

        public void Conectar() { _conexao.Open(); }

        public void Desconectar() { _conexao.Close(); }

        public void Editar(Pessoa pessoa, Pessoa atualizada)
        {
            if (pessoa == null)
            {
                return;
            }

            if (pessoa is Aluno aluno && atualizada is Aluno alunoAtualizado)
            {
                AtualizarAluno(aluno, alunoAtualizado);
            }
            else if (pessoa is Professor professor && atualizada is Professor professorAtualizado)
            {
                AtualizarProfessor(professor, professorAtualizado);
            }
            else if (pessoa is Faxineiro faxineiro && atualizada is Faxineiro faxineiroAtualizado)
            {
                AtualizarFaxineiro(faxineiro, faxineiroAtualizado);
            }
        }

        public void Inserir(Pessoa pessoa)
        {
            NpgsqlCommand cmd = null;

            if (pessoa is Aluno aluno) { cmd = ComandoInserirAluno(aluno); }
            else if (pessoa is Professor professor) { cmd = ComandoInserirProfessor(professor); }
            else if (pessoa is Faxineiro faxineiro) { cmd = ComandoInserirFaxineiro(faxineiro); }

            cmd?.ExecuteNonQuery();
        }

        public void Remover(Pessoa pessoa)
        {
            if (pessoa == null)
            {
                return;
            }

            string tabela = string.Empty;

            if (pessoa is Aluno) { tabela = "alunos"; }
            else if (pessoa is Professor) { tabela = "professores"; }
            else if (pessoa is Faxineiro) { tabela = "faxineiros"; }
            else { return; }

            var cmd = new NpgsqlCommand($"DELETE FROM {tabela} WHERE " +
                                        $"id='{pessoa.Id}'", _conexao);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }

        private void AtualizarAluno(Aluno aluno, Aluno atualizado)
        {
            var cmd = new NpgsqlCommand(
                $"UPDATE alunos " +
                $"SET nome='{atualizado.Nome}', " +
                    $"sobrenome='{atualizado.Sobrenome}', " +
                    $"data_nascimento='{atualizado.DataNascimento:yyyy-MM-dd HH:mm:ss}', " +
                    $"matricula={atualizado.Matricula}" +
                $"WHERE id='{aluno.Id}'", _conexao);
            cmd.ExecuteNonQuery();
        }

        private void AtualizarFaxineiro(Faxineiro faxineiro, Faxineiro atualizado)
        {
            var cmd = new NpgsqlCommand(
                $"UPDATE faxineiros " +
                $"SET nome='{atualizado.Nome}', " +
                    $"sobrenome='{atualizado.Sobrenome}', " +
                    $"data_nascimento='{atualizado.DataNascimento:yyyy-MM-dd HH:mm:ss}', " +
                    $"salario={atualizado.Salario} " +
                $"WHERE id='{faxineiro.Id}'", _conexao);
            cmd.ExecuteNonQuery();
        }

        private void AtualizarProfessor(Professor professor, Professor atualizado)
        {
            var cmd = new NpgsqlCommand(
                $"UPDATE professores " +
                $"SET nome='{atualizado.Nome}', " +
                    $"sobrenome='{atualizado.Sobrenome}', " +
                    $"data_nascimento='{atualizado.DataNascimento:yyyy-MM-dd HH:mm:ss}', " +
                    $"salario={atualizado.Salario}, " +
                    $"disciplina='{atualizado.Disciplina}' " +
                $"WHERE id='{professor.Id}'", _conexao);
            cmd.ExecuteNonQuery();
        }
        private void CarregarAlunos(Collection<Pessoa> list)
        {
            var cmd = new NpgsqlCommand("SELECT * FROM alunos;", _conexao);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Aluno al = new Aluno(reader.GetString(reader.GetOrdinal("id")))
                {
                    Nome = reader.GetString(reader.GetOrdinal("nome")),
                    Sobrenome = reader.GetString(reader.GetOrdinal("sobrenome")),
                    DataNascimento = reader.GetDateTime(reader.GetOrdinal("data_nascimento")),
                    Matricula = reader.GetInt32(reader.GetOrdinal("matricula"))
                };
                list.Add(al);
            }

            reader.Close();
            cmd.Dispose();
        }

        private void CarregarFaxineiros(Collection<Pessoa> list)
        {
            var cmd = new NpgsqlCommand("SELECT * FROM faxineiros;", _conexao);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Faxineiro al = new Faxineiro(reader.GetString(reader.GetOrdinal("id")))
                {
                    Nome = reader.GetString(reader.GetOrdinal("nome")),
                    Sobrenome = reader.GetString(reader.GetOrdinal("sobrenome")),
                    DataNascimento = reader.GetDateTime(reader.GetOrdinal("data_nascimento")),
                    Salario = reader.GetFloat(reader.GetOrdinal("salario"))
                };
                list.Add(al);
            }

            reader.Close();
            cmd.Dispose();
        }

        private void CarregarProfessores(Collection<Pessoa> list)
        {
            var cmd = new NpgsqlCommand("SELECT * FROM professores;", _conexao);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Professor al = new Professor(reader.GetString(reader.GetOrdinal("id")))
                {
                    Nome = reader.GetString(reader.GetOrdinal("nome")),
                    Sobrenome = reader.GetString(reader.GetOrdinal("sobrenome")),
                    DataNascimento = reader.GetDateTime(reader.GetOrdinal("data_nascimento")),
                    Salario = reader.GetFloat(reader.GetOrdinal("salario")),
                    Disciplina = reader.GetString(reader.GetOrdinal("disciplina"))
                };
                list.Add(al);
            }

            reader.Close();
            cmd.Dispose();
        }
        private NpgsqlCommand ComandoInserirAluno(Aluno aluno)
        {
            return new NpgsqlCommand(
                $"INSERT INTO \"alunos\" (id, nome, sobrenome, data_nascimento, matricula) VALUES (" +
                $"'{aluno.Id}', " +
                $"'{aluno.Nome}', " +
                $"'{aluno.Sobrenome}', " +
                $"'{new NpgsqlDateTime(aluno.DataNascimento)}', " +
                $"'{aluno.Matricula}');", _conexao);
        }

        private NpgsqlCommand ComandoInserirFaxineiro(Faxineiro faxineiro)
        {
            return new NpgsqlCommand($"INSERT INTO \"faxineiros\" (id, nome, sobrenome, data_nascimento, salario) VALUES (" +
                $"'{faxineiro.Id}', " +
                $"'{faxineiro.Nome}', " +
                $"'{faxineiro.Sobrenome}', " +
                $"'{new NpgsqlDateTime(faxineiro.DataNascimento)}', " +
                $"'{faxineiro.Salario}');", _conexao);
        }

        private NpgsqlCommand ComandoInserirProfessor(Professor professor)
        {
            return new NpgsqlCommand($"INSERT INTO \"professores\" (id, nome, sobrenome, data_nascimento, salario, disciplina) VALUES (" +
                $"'{professor.Id}', " +
                $"'{professor.Nome}', " +
                $"'{professor.Sobrenome}', " +
                $"'{new NpgsqlDateTime(professor.DataNascimento)}', " +
                $"'{professor.Salario}', " +
                $"'{professor.Disciplina}');", _conexao);
        }
    }
}
