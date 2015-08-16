using System;
using System.Windows.Input;

namespace KittyFeeder
{
	public class RelayCommand : ICommand
	{
		Action<object> _command;

		public RelayCommand(Action<object> command)
		{
			_command = command;
		}

		public void Execute(object parameter)
		{
			_command(parameter);
		}

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public event EventHandler CanExecuteChanged;
	}
}

