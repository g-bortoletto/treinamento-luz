using System;

namespace SistemaEscola.Model
{
	public class Professor : Pessoa
	{
		private float _salario;
		private string _disciplina = string.Empty;

		public float Salario { get { return _salario; } set { _salario = value; } }
		public string Disciplina { get { return _disciplina; } set { _disciplina = value; } }

		public Professor() :base() { }

		private void Init(string nome,
						 string sobrenome,
						 DateTime dataNascimento,
						 float salario,
						 string disciplina)
		{
			Nome = nome;
			Sobrenome = sobrenome;
			DataNascimento = dataNascimento;
			Salario = salario;
			Disciplina = disciplina;
		}

		public Professor(Pessoa pessoa,
						 float salario,
						 string disciplina)
		: base()
		{
			Init(pessoa.Nome,
				 pessoa.Sobrenome,
				 pessoa.DataNascimento,
				 salario,
				 disciplina);
		}

		public Professor(string nome,
						 string sobrenome,
						 DateTime dataNascimento,
						 float salario,
						 string disciplina)
		: base()
		{
			Init(nome,
				 sobrenome,
				 dataNascimento,
				 salario,
				 disciplina);
		}

	}
}
