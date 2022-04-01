using SistemaEscola.Utils;
using System;
using System.Collections.ObjectModel;

namespace SistemaEscola.Model
{
    public class Aluno : Pessoa
    {
        private int _matricula;
        public int Matricula { get { return _matricula; } set { _matricula = value; } }


        public Aluno() : base() { }

        public Aluno(string id) : base(id) { }
    }
}
