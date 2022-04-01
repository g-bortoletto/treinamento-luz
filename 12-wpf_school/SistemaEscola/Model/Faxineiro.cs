using System;

namespace SistemaEscola.Model
{
	public class Faxineiro : Pessoa
	{
		private float _salario;

		public float Salario { get { return _salario; } set { _salario = value; } }


		public Faxineiro() : base() { }

		public Faxineiro(string id) : base(id) { }
	}
}
