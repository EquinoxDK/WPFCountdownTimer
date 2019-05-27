using CountdownShared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CountdownShared.Commands
{
    class TimerSheetViewModelCloseCommand : ICommand
    {
        public TimerSheetViewModelCloseCommand(TimerSheetViewModel ViewModel)
        {
            viewModel = ViewModel;

        }

        private TimerSheetViewModel viewModel;

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
