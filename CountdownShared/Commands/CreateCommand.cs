using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace CountdownShared.Commands
{
    class CreateCommand<T> : ICommand where T : IViewModel
    {
        public CreateCommand(T viewModel)
        {
            this.viewModel = viewModel;
        }

        private T viewModel;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return viewModel.CanUpdate;
        }

        public void Execute(object parameter)
        {
            viewModel.SaveChanged();
        }
    }
}
