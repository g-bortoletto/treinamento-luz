using Npgsql;
using NpgsqlTypes;
using SistemaEscola.Model;
using System;
using System.Collections.ObjectModel;
using System.Data;

namespace SistemaEscola.Utils
{
    public class PostgreSql : IDbCrud
    {
        private NpgsqlConnection _conexao;
        private string _stringConexao;

        public IDbConnection Conexao { get => _conexao; set => _conexao = value as NpgsqlConnection; }
        public string StringConexao { get => _stringConexao; set => _stringConexao = value; }

        public PostgreSql(string host, string usuario, string senha, string bancoDeDados, string port)
        {
            var str = new NpgsqlConnectionStringBuilder();
            str.Host = host;
            str.Username = usuario;
            str.Password = senha;
            str.Database = bancoDeDados;
            str.Port = int.Parse(port);

            StringConexao = str.ConnectionString;
            Conexao = new NpgsqlConnection(StringConexao);

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
            NpgsqlCommand cmd = null;
            if (typeOfPessoa == typeof(Aluno))
            {
                cmd = new NpgsqlCommand($"INSERT INTO \"alunos\" (nome, sobrenome, data_nascimento, matricula) VALUES (" +
                    $"'{pessoa.Nome}', " +
                    $"'{pessoa.Sobrenome}', " +
                    $"'{new NpgsqlDateTime(pessoa.DataNascimento)}', " +
                    $"'{(pessoa as Aluno).Matricula}');", _conexao);

            }
            else if (typeOfPessoa == typeof(Professor))
            {
                cmd = new NpgsqlCommand($"INSERT INTO \"professores\" (nome, sobrenome, data_nascimento, salario, disciplina) VALUES (" +
                    $"'{pessoa.Nome}', " +
                    $"'{pessoa.Sobrenome}', " +
                    $"'{new NpgsqlDateTime(pessoa.DataNascimento)}', " +
                    $"'{(pessoa as Professor).Salario}', " +
                    $"'{(pessoa as Professor).Disciplina}');", _conexao);
            }
            else if (typeOfPessoa == typeof(Faxineiro))
            {
                cmd = new NpgsqlCommand($"INSERT INTO \"faxineiros\" (nome, sobrenome, data_nascimento, salario) VALUES (" +
                    $"'{pessoa.Nome}', " +
                    $"'{pessoa.Sobrenome}', " +
                    $"'{new NpgsqlDateTime(pessoa.DataNascimento)}', " +
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

            NpgsqlCommand cmd = new NpgsqlCommand($"DELETE FROM \"alunos\" WHERE " +
                $"nome='{pessoa.Nome}' AND " +
                $"sobrenome='{pessoa.Sobrenome}'", _conexao);
            cmd.ExecuteNonQuery();
            cmd.Dispose();

            cmd = new NpgsqlCommand($"DELETE FROM \"professores\" WHERE " +
                $"nome='{pessoa.Nome}' AND " +
                $"sobrenome='{pessoa.Sobrenome}'", _conexao);
            cmd.ExecuteNonQuery();
            cmd.Dispose();

            cmd = new NpgsqlCommand($"DELETE FROM \"faxineiros\" WHERE " +
                $"nome='{pessoa.Nome}' AND " +
                $"sobrenome='{pessoa.Sobrenome}'", _conexao);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }

        public void CarregarDados(Collection<Pessoa> list)
        {
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM \"alunos\";", _conexao);
            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Aluno al = new Aluno();
                al.Nome = reader.GetString(reader.GetOrdinal("nome"));
                al.Sobrenome = reader.GetString(reader.GetOrdinal("sobrenome"));
                al.DataNascimento = (DateTime)reader.GetDate(reader.GetOrdinal("data_nascimento"));
                al.Matricula = reader.GetInt32(reader.GetOrdinal("matricula"));
                list.Add(al);
            }

            reader.Close();
            cmd.Dispose();

            cmd = new NpgsqlCommand("SELECT * FROM \"faxineiros\";", _conexao);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Faxineiro al = new Faxineiro();
                al.Nome = reader.GetString(reader.GetOrdinal("nome"));
                al.Sobrenome = reader.GetString(reader.GetOrdinal("sobrenome"));
                al.DataNascimento = (DateTime)reader.GetDate(reader.GetOrdinal("data_nascimento"));
                al.Salario = reader.GetFloat(reader.GetOrdinal("salario"));
                list.Add(al);
            }

            reader.Close();
            cmd.Dispose();

            cmd = new NpgsqlCommand("SELECT * FROM \"professores\";", _conexao);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Professor al = new Professor();
                al.Nome = reader.GetString(reader.GetOrdinal("nome"));
                al.Sobrenome = reader.GetString(reader.GetOrdinal("sobrenome"));
                al.DataNascimento = (DateTime)reader.GetDate(reader.GetOrdinal("data_nascimento"));
                al.Salario = reader.GetFloat(reader.GetOrdinal("salario"));
                al.Disciplina = reader.GetString(reader.GetOrdinal("disciplina"));
                list.Add(al);
            }

            reader.Close();
            cmd.Dispose();
        }
    }
}
