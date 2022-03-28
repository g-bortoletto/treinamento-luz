using SistemaEscola.Model;
using System;
using System.Data;
using System.Threading.Tasks;

namespace SistemaEscola.Utils
{
	public interface IDbCrud
	{
		IDbConnection Conexao { get; set; }
		string StringConexao { get; set; }

		void Conectar();
		void Desconectar();

		void Inserir(Pessoa pessoa, Type typeOfPessoa);
		void Procurar(Pessoa pessoa);
		void Editar(Pessoa pessoa, Pessoa atualizada);
		void Remover(Pessoa pessoa);
	}
}
