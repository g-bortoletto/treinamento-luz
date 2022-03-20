using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SchoolApp.ViewModels
{
	internal class DelegateCommand : ICommand
	{
		public Action<object> m_execute;
		public Func<object, bool> m_canExecute;

		public event EventHandler? CanExecuteChanged;

		public DelegateCommand(Action<object> execute, Func<object, bool> canExecute = null)
		{
			m_execute = execute;
			m_canExecute = canExecute;
		}

		public bool CanExecute(object? parameter)
		{
			return m_canExecute == null || m_canExecute(parameter);
		}

		public void Execute(object? parameter)
		{
			m_execute(parameter);
		}
	}
}
