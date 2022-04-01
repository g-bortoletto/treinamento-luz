using SistemaEscola.Model;
using System;
using System.Collections.ObjectModel;

namespace SistemaEscola.Utils
{
	public interface IDbCrud
	{
        void CarregarDados(Collection<Pessoa> cp);

        void Conectar();
        void Desconectar();

        void Editar(Pessoa pessoa, Pessoa atualizada);

        void Inserir(Pessoa pessoa);
        void Remover(Pessoa pessoa);
	}
}
