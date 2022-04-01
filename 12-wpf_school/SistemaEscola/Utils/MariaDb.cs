using MySql.Data.MySqlClient;
using SistemaEscola.Model;
using System.Collections.ObjectModel;

namespace SistemaEscola.Utils
{
    public class MariaDb : IDbCrud
    {
        private readonly MySqlConnection _conexao;

        public MariaDb(string host, string usuario, string senha, string bancoDeDados)
        {
            var str = new MySqlConnectionStringBuilder();
            str.Server = host;
            str.UserID = usuario;
            str.Password = senha;
            str.Database = bancoDeDados;
            str.Port = 3306;

            _conexao = new MySqlConnection(str.ConnectionString);

            Conectar();
        }

        public void CarregarDados(Collection<Pessoa> list)
        {
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM alunos;", _conexao);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Aluno al = new Aluno();
                al.Nome = reader.GetString(reader.GetOrdinal("nome"));
                al.Sobrenome = reader.GetString(reader.GetOrdinal("sobrenome"));
                al.DataNascimento = reader.GetDateTime(reader.GetOrdinal("data_nascimento"));
                al.Matricula = reader.GetInt32(reader.GetOrdinal("matricula"));
                list.Add(al);
            }

            reader.Close();
            cmd.Dispose();

            cmd = new MySqlCommand("SELECT * FROM faxineiros;", _conexao);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Faxineiro al = new Faxineiro();
                al.Nome = reader.GetString(reader.GetOrdinal("nome"));
                al.Sobrenome = reader.GetString(reader.GetOrdinal("sobrenome"));
                al.DataNascimento = reader.GetDateTime(reader.GetOrdinal("data_nascimento"));
                al.Salario = reader.GetFloat(reader.GetOrdinal("salario"));
                list.Add(al);
            }

            reader.Close();
            cmd.Dispose();

            cmd = new MySqlCommand("SELECT * FROM professores;", _conexao);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Professor al = new Professor();
                al.Nome = reader.GetString(reader.GetOrdinal("nome"));
                al.Sobrenome = reader.GetString(reader.GetOrdinal("sobrenome"));
                al.DataNascimento = reader.GetDateTime(reader.GetOrdinal("data_nascimento"));
                al.Salario = reader.GetFloat(reader.GetOrdinal("salario"));
                al.Disciplina = reader.GetString(reader.GetOrdinal("disciplina"));
                list.Add(al);
            }

            reader.Close();
            cmd.Dispose();
        }

        public void Conectar()
        {
            _conexao.Open();
        }

        public void Desconectar()
        {
            _conexao.Close();
        }

        public void Editar(Pessoa pessoa, Pessoa atualizada)
        {
            throw new System.NotImplementedException();
        }

        public void Inserir(Pessoa pessoa)
        {
            MySqlCommand cmd = null;

            if (pessoa is Aluno aluno) { cmd = ComandoInserirAluno(aluno); }
            else if (pessoa is Professor professor) { cmd = ComandoInserirProfessor(professor); }
            else if (pessoa is Faxineiro faxineiro) { cmd = ComandoInserirFaxineiro(faxineiro); }

            cmd?.ExecuteNonQuery();
        }

        public void Procurar(Pessoa pessoa)
        {
            throw new System.NotImplementedException();
        }

        public void Remover(Pessoa pessoa)
        {
            if (pessoa == null)
            {
                return;
            }

            MySqlCommand cmd = new MySqlCommand($"DELETE FROM alunos WHERE " +
                $"nome='{pessoa.Nome}' AND " +
                $"sobrenome='{pessoa.Sobrenome}'", _conexao);
            cmd.ExecuteNonQuery();
            cmd.Dispose();

            cmd = new MySqlCommand($"DELETE FROM professores WHERE " +
                $"nome='{pessoa.Nome}' AND " +
                $"sobrenome='{pessoa.Sobrenome}'", _conexao);
            cmd.ExecuteNonQuery();
            cmd.Dispose();

            cmd = new MySqlCommand($"DELETE FROM faxineiros WHERE " +
                $"nome='{pessoa.Nome}' AND " +
                $"sobrenome='{pessoa.Sobrenome}'", _conexao);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }

        private MySqlCommand ComandoInserirAluno(Aluno aluno)
        {
            return new MySqlCommand("INSERT INTO alunos (id, nome, sobrenome, data_nascimento, matricula) VALUES (" +
                                    $"'{aluno.Id}', " +
                                    $"'{aluno.Nome}', " +
                                    $"'{aluno.Sobrenome}', " +
                                    $"'{aluno.DataNascimento:yyyy-MM-dd HH:mm:ss}', " +
                                    $"'{aluno.Matricula}');", _conexao);
        }

        private MySqlCommand ComandoInserirFaxineiro(Faxineiro faxineiro)
        {
            return new MySqlCommand("INSERT INTO faxineiros (id, nome, sobrenome, data_nascimento, salario) VALUES (" +
                                    $"'{faxineiro.Id}', " +
                                    $"'{faxineiro.Nome}', " +
                                    $"'{faxineiro.Sobrenome}', " +
                                    $"'{faxineiro.DataNascimento:yyyy-MM-dd HH:mm:ss}', " +
                                    $"'{faxineiro.Salario}');", _conexao);
        }

        private MySqlCommand ComandoInserirProfessor(Professor professor)
        {
            return new MySqlCommand("INSERT INTO professores (id, nome, sobrenome, data_nascimento, salario, disciplina) VALUES (" +
                                    $"'{professor.Id}', " +
                                    $"'{professor.Nome}', " +
                                    $"'{professor.Sobrenome}', " +
                                    $"'{professor.DataNascimento:yyyy-MM-dd HH:mm:ss}', " +
                                    $"'{professor.Salario}', " +
                                    $"'{professor.Disciplina}');", _conexao);
        }
    }
}