using SistemaEscola.Utils;
using System;
using System.Collections.ObjectModel;

namespace SistemaEscola.Model
{
	public class Pessoa
	{
		protected string _id;
		protected DateTime _dataNascimento = DateTime.Now;
		protected string _nome = string.Empty;
		protected string _sobrenome = string.Empty;

		public string Id { get { return _id;} }
		public DateTime DataNascimento { get { return _dataNascimento; } set { _dataNascimento = value;} }
		public string Nome { get { return _nome;} set { _nome = value;} }
		public string Sobrenome { get { return _sobrenome;} set { _sobrenome = value;} }

		protected void Init(string id = null)
        {
			_id = id ?? Guid.NewGuid().ToString();
        }

		public Pessoa()
		{
			Init();
		}

		public Pessoa(string id)
        {
			Init(id);
        }


		public void InserirEm(Collection<Pessoa> lista, IDbCrud bd = null)
		{
			lista.Add(this);
			if (bd != null)
            {
				bd.Inserir(this);
            }
		}

		public void SairDe(Collection<Pessoa> lista, IDbCrud bd = null)
		{
			lista.Remove(this);
			if (bd != null)
            {
				bd.Remover(this);
            }
		}
	}
}
