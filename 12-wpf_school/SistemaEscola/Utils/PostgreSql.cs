using Npgsql;
using NpgsqlTypes;
using SistemaEscola.Model;
using System;
using System.Collections.ObjectModel;

namespace SistemaEscola.Utils
{
    public class PostgreSql : IDbCrud
    {
        private readonly NpgsqlConnection _conexao;

        public PostgreSql(string host, string usuario, string senha, string bancoDeDados)
        {
            var str = new NpgsqlConnectionStringBuilder();
            str.Host = host;
            str.Username = usuario;
            str.Password = senha;
            str.Database = bancoDeDados;
            str.Port = 5432;

            _conexao = new NpgsqlConnection(str.ConnectionString);

            Conectar();
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
            NpgsqlCommand cmd = null;
            if (pessoa is Aluno aluno)
            {
                cmd = ComandoInserirAluno(aluno);

            }
            else if (pessoa is Professor professor)
            {
                cmd = ComandoInserirProfessor(professor);
            }
            else if (pessoa is Faxineiro faxineiro)
            {
                cmd = ComandoInserirFaxineiro(faxineiro);
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

        private NpgsqlCommand ComandoInserirAluno(Aluno aluno)
        {
            return new NpgsqlCommand($"INSERT INTO \"alunos\" (nome, sobrenome, data_nascimento, matricula) VALUES (" +
                $"'{aluno.Nome}', " +
                $"'{aluno.Sobrenome}', " +
                $"'{new NpgsqlDateTime(aluno.DataNascimento)}', " +
                $"'{aluno.Matricula}');", _conexao);
        }

        private NpgsqlCommand ComandoInserirFaxineiro(Faxineiro faxineiro)
        {
            return new NpgsqlCommand($"INSERT INTO \"faxineiros\" (nome, sobrenome, data_nascimento, salario) VALUES (" +
                $"'{faxineiro.Nome}', " +
                $"'{faxineiro.Sobrenome}', " +
                $"'{new NpgsqlDateTime(faxineiro.DataNascimento)}', " +
                $"'{faxineiro.Salario}');", _conexao);
        }

        private NpgsqlCommand ComandoInserirProfessor(Professor professor)
        {
            return new NpgsqlCommand($"INSERT INTO \"professores\" (nome, sobrenome, data_nascimento, salario, disciplina) VALUES (" +
                $"'{professor.Nome}', " +
                $"'{professor.Sobrenome}', " +
                $"'{new NpgsqlDateTime(professor.DataNascimento)}', " +
                $"'{professor.Salario}', " +
                $"'{professor.Disciplina}');", _conexao);
        }
    }
}
