using System;

namespace SistemaEscola.Model
{
	public class Faxineiro : Pessoa
	{
		private float _salario;

		public float Salario { get { return _salario; } set { _salario = value; } }


		public Faxineiro() : base() { }

		private void Init(string nome,
						  string sobrenome,
						  DateTime dataNascimento,
						  float salario)
		{
			Nome = nome;
			Sobrenome = sobrenome;
			DataNascimento = dataNascimento;
			Salario = salario;
		}

		public Faxineiro(Pessoa pessoa, float salario) : base()
		{
			Init(pessoa.Nome,
				 pessoa.Sobrenome,
				 pessoa.DataNascimento,
				 salario);
		}

		public Faxineiro(string nome,
						 string sobrenome,
						 DateTime dataNascimento,
						 float salario) : base()
        {
			Init(nome, sobrenome, dataNascimento, salario);
        }
	}
}
