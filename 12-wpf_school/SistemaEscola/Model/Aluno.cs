using SistemaEscola.Utils;
using System;
using System.Collections.ObjectModel;

namespace SistemaEscola.Model
{
    public class Aluno : Pessoa
    {
        private int _matricula;
        public int Matricula { get { return _matricula; } set { _matricula = value; } }


        public Aluno() : base()
        {
        }

        public Aluno(Pessoa pessoa,
                     int matricula)
        : base()
        {
            Init(pessoa.Nome,
				 pessoa.Sobrenome,
				 pessoa.DataNascimento,
				 matricula);
        }

        public Aluno(string nome,
					 string sobrenome,
					 DateTime dataNascimento,
					 int matricula)
        :base()
        {
            Init(nome,
				 sobrenome,
				 dataNascimento,
				 matricula);
        }

        private void Init(string nome,
						  string sobrenome,
						  DateTime dataNascimento,
						  int matricula)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            DataNascimento = dataNascimento;
            Matricula = matricula;
        }
    }
}
