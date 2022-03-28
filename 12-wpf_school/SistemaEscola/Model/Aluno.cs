using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEscola.Model
{
	public class Aluno : Pessoa
	{
		private int _matricula;
		public int Matricula { get { return _matricula; } set { _matricula = value; } }
	}
}
