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
                cmd = new NpgsqlCommand($"INSERT INTO \"Alunos\" (nome, sobrenome, datanascimento, matricula) VALUES (" +
                    $"'{pessoa.Nome}', " +
                    $"'{pessoa.Sobrenome}', " +
                    $"'{new NpgsqlDateTime(pessoa.DataNascimento)}', " +
                    $"'{(pessoa as Aluno).Matricula}');", _conexao);

            }
            else if (typeOfPessoa == typeof(Professor))
            {
                cmd = new NpgsqlCommand($"INSERT INTO \"Professores\" (nome, sobrenome, datanascimento, salario, disciplina) VALUES (" +
                    $"'{pessoa.Nome}', " +
                    $"'{pessoa.Sobrenome}', " +
                    $"'{new NpgsqlDateTime(pessoa.DataNascimento)}', " +
                    $"'{(pessoa as Professor).Salario}', " +
                    $"'{(pessoa as Professor).Disciplina}');", _conexao);
            }
            else if (typeOfPessoa == typeof(Faxineiro))
            {
                cmd = new NpgsqlCommand($"INSERT INTO \"Faxineiros\" (nome, sobrenome, datanascimento, salario) VALUES (" +
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

            NpgsqlCommand cmd = new NpgsqlCommand($"REMOVE FROM Alunos WHERE " +
                $"Nome='{pessoa.Nome}' AND " +
                $"Sobrenome='{pessoa.Sobrenome}'", _conexao);
            cmd.ExecuteNonQuery();
            cmd = new NpgsqlCommand($"REMOVE FROM Professores WHERE " +
                $"Nome='{pessoa.Nome}' AND " +
                $"Sobrenome='{pessoa.Sobrenome}'", _conexao);
            cmd.ExecuteNonQuery();
            cmd = new NpgsqlCommand($"REMOVE FROM Faxineiros WHERE " +
                $"Nome='{pessoa.Nome}' AND " +
                $"Sobrenome='{pessoa.Sobrenome}'", _conexao);
            cmd.ExecuteNonQuery();
        }

        public void CarregarDados(Collection<Pessoa> list)
        {
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM \"Alunos\";", _conexao);
            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Aluno al = new Aluno();
                al.Nome = reader.GetString(reader.GetOrdinal("nome"));
                al.Sobrenome = reader.GetString(reader.GetOrdinal("sobrenome"));
                al.DataNascimento = (DateTime)reader.GetDate(reader.GetOrdinal("datanascimento"));
                al.Matricula = reader.GetInt32(reader.GetOrdinal("matricula"));
                list.Add(al);
            }

            reader.Close();
            cmd.Dispose();
        }
    }
}
