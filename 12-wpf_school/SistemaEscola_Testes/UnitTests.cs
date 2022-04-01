using SistemaEscola.Model;
using System;
using Xunit;

namespace SistemaEscola_Testes
{
    public class UnitTests
    {
        SistemaEscola.Model.SistemaEscola se = new();

        [Fact]
        public void ViewModel_AdicionarAluno_Sucesso()
        {
            // Arrange
            Aluno al = new Aluno();
            al.Nome = "Norberto";
            al.Sobrenome = "Nascimento";
            al.DataNascimento = DateTime.Parse("1990-03-17 13:55:12");
            al.Matricula = 33456;

            // Act
            se.AdicionarPessoa(al);
            
            // Assert
            Assert.NotNull(al.Id);
            Assert.NotEmpty(al.Id);
            Assert.Contains<Pessoa>(al, se.Pessoas);
        }

        [Fact]
        public void ViewModel_RemoverAluno_Sucesso()
        {
            // Arrange
            // Act
            // Assert
        }

        [Fact]
        public void ViewModel_EditarAluno_Sucesso()
        {
            // Arrange
            // Act
            // Assert
        }
    }
}