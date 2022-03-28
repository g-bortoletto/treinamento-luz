using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEscola.Model
{
	public class Professor : Pessoa
	{
		private float _salario;
		private string _disciplina = string.Empty;

		public float Salario { get { return _salario; } set { _salario = value; } }
		public string Disciplina { get { return _disciplina; } set { _disciplina = value;} }
	}
}
