using System;
using System.Windows.Input;

namespace SistemaEscola.Utils
{
	public class ExecutarComando : ICommand
	{
		private readonly Action<object> m_executar;
		private readonly Func<object, bool> m_podeExecutar;

		public event EventHandler CanExecuteChanged;

		public ExecutarComando(Action<object> executar, Func<object, bool> podeExecutar = null)
		{
			m_executar = executar;
			m_podeExecutar = podeExecutar;
		}

		public bool PodeExecutar(object param) => CanExecute(param);
		public bool CanExecute(object parameter) => m_podeExecutar == null || m_podeExecutar(parameter);
		
		public void Executar(object param) => Execute(param);
		public void Execute(object parameter) => m_executar(parameter);
	}
}
