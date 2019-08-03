using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CountdownShared.Commands
{
    public class DelegateCommandResolver : ICommand
    {
        private readonly Action execute;
        private readonly Func<bool> canExecute;

        public DelegateCommandResolver(Action execute) : this(execute, null)
        {
        }

        public DelegateCommandResolver(Action execute, Func<bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return canExecute != null ? canExecute() : true;
        }

        public void Execute(object parameter)
        {
            execute?.Invoke();
        }
    }
}
