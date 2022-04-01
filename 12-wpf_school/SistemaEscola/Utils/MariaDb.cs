using System;
using System.Collections.ObjectModel;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using SistemaEscola.Model;

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
            str.Port = (uint)3306;

            _conexao = new MySqlConnection(str.ConnectionString);

            Conectar();
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

        public void Inserir(Pessoa pessoa, Type typeOfPessoa)
        {
            MySqlCommand cmd = null;
            if (typeOfPessoa == typeof(Aluno))
            {
                cmd = new MySqlCommand("INSERT INTO alunos (id, nome, sobrenome, data_nascimento, matricula) VALUES (" +
                                        $"'{pessoa.Id}', " +
                                        $"'{pessoa.Nome}', " +
                                        $"'{pessoa.Sobrenome}', " +
                                        $"'{pessoa.DataNascimento.ToString("yyyy-MM-dd HH:mm:ss")}', " +
                                        $"'{(pessoa as Aluno).Matricula}');", _conexao);

            }
            else if (typeOfPessoa == typeof(Professor))
            {
                cmd = new MySqlCommand("INSERT INTO professores (id, nome, sobrenome, data_nascimento, salario, disciplina) VALUES (" +
                                        $"'{pessoa.Id}', " +
                                        $"'{pessoa.Nome}', " +
                                        $"'{pessoa.Sobrenome}', " +
                                        $"'{pessoa.DataNascimento.ToString("yyyy-MM-dd HH:mm:ss")}', " +
                                        $"'{(pessoa as Professor).Salario}', " +
                                        $"'{(pessoa as Professor).Disciplina}');", _conexao);
            }
            else if (typeOfPessoa == typeof(Faxineiro))
            {
                cmd = new MySqlCommand("INSERT INTO faxineiros (id, nome, sobrenome, data_nascimento, salario) VALUES (" +
                                        $"'{pessoa.Id}', " +
                                        $"'{pessoa.Nome}', " +
                                        $"'{pessoa.Sobrenome}', " +
                                        $"'{pessoa.DataNascimento.ToString("yyyy-MM-dd HH:mm:ss")}', " +
                                        $"'{(pessoa as Faxineiro).Salario}');", _conexao);
            }

            cmd.ExecuteNonQuery();
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
    }
}
