using System;
using System.Windows.Input;


namespace CountdownShared.Commands
{
    class CommandResolver<T> : ICommand where T : IViewModel
    {
        public CommandResolver(T viewModel)
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
