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

		public Professor(string id) : base(id)
		{
		}
	}
}
