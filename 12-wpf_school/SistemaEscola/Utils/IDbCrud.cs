using SistemaEscola.Model;
using System;
using System.Collections.ObjectModel;

namespace SistemaEscola.Utils
{
	public interface IDbCrud
	{
		void Conectar();
		void Desconectar();

		void Inserir(Pessoa pessoa, Type typeOfPessoa);
		void CarregarDados(Collection<Pessoa> cp);
		void Editar(Pessoa pessoa, Pessoa atualizada);
		void Remover(Pessoa pessoa);
	}
}
