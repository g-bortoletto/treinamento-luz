using SistemaEscola.Model;

namespace SistemaEscola.Utils
{
	internal interface IDbCrud
	{
		object Conexao { get; set; }
		string StringConexao { get; set; }

		void Conectar();
		void Desconectar();

		void Inserir(Pessoa pessoa);
		void Procurar(Pessoa pessoa);
		void Editar(Pessoa pessoa, Pessoa atualizado)
		void Remover(Pessoa pessoa);
	}
}
