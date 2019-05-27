using CountdownShared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CountdownShared.Commands
{
    internal class CountdownViewModelCreateCommand : ICommand
    {
        public CountdownViewModelCreateCommand(CountdownViewModel viewModel)
        {
            _viewModel = viewModel;

        }

        private CountdownViewModel _viewModel;



        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _viewModel.CanUpdate;
        }

        public void Execute(object parameter)
        {
            _viewModel.SaveChanges();
        }
    }
}
