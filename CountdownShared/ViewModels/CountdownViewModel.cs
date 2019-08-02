using CountdownShared.Commands;
using CountdownShared.Models;
using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Timers;
using System.Windows.Input;
using System.Linq;

namespace CountdownShared.ViewModels
{
    public class CountdownViewModel : ViewModel
    {
        public CountdownViewModel(SaveList saveList)
        {
            if (saveList != null)
            {
                DoSaveList += saveList;
            }
            countdown = new CountdownModel();
            timer = new AppTimer(200);
            timer.AfterTick += AfterTick;
            timerSheets = new ObservableCollection<TimerSheetViewModel>();
            CreateCommand = new CommandResolver<CountdownViewModel>(this);
            System.IO.File.Create("timer.txt");
        }

        private AppTimer timer;
        private bool CanSave = true;
        
        private ObservableCollection<TimerSheetViewModel> timerSheets;
        public ObservableCollection<TimerSheetViewModel> TimerSheets
        {
            get { return timerSheets; }
        }

        private CountdownModel countdown;
        public CountdownModel Countdown
        {
            get { return countdown; }
        }

        public ICommand CreateCommand { get; private set; }
        public bool CanUpdate
        {
            get
            {
                if (Countdown == null)
                {
                    return false;
                }
                return
                    !string.IsNullOrWhiteSpace(Countdown.Description) &&
                    (Countdown.Hours >= 0 && Countdown.Hours <= 23) &&
                    (Countdown.Minutes >= 0 && Countdown.Minutes <= 59) &&
                    (Countdown.Seconds >= 0 && Countdown.Seconds <= 59);
            }
        }

        public void SaveChanged()
        {
            TimerSheetViewModel tsvm = new TimerSheetViewModel(Countdown.Hours, Countdown.Minutes, Countdown.Seconds, Countdown.Description);
            timer.Tick += tsvm.Tick;
            tsvm.DoRemoveButton = RemoveButton;
            TimerSheets.Add(tsvm);
        }

        private void RemoveButton(TimerSheetViewModel viewModel)
        {
            timerSheets.Remove(viewModel);
        }

        private void AfterTick(ElapsedEventArgs e)
        {
            if (DoSaveList != null)
            {
                DoSaveList(TimerSheets.ToArray());

            }
        }

        public delegate void SaveList(TimerSheetViewModel[] sheets);
        public SaveList DoSaveList;
    }
}
