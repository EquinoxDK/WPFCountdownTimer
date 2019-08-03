using CountdownShared.Commands;
using CountdownShared.Models;
using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Timers;
using System.Windows.Input;
using System.Linq;
using System.Configuration;

namespace CountdownShared.ViewModels
{
    public class CountdownViewModel : IViewModel
    {
        public CountdownViewModel(SaveList saveList)
        {
            OutputFilename = ConfigurationManager.AppSettings["Filename"];
            Countdown = new CountdownModel();
            timer = new AppTimer(200);
            timer.AfterTick += AfterTick;
            TimerSheets = new ObservableCollection<TimerSheetViewModel>();
            CreateCommand = new DelegateCommandResolver(SaveChanged, () => CanUpdate);
            System.IO.File.Create(OutputFilename);
            DoSaveList += saveList;
        }

        private readonly string OutputFilename;
        private readonly AppTimer timer;
        private bool CanSave = true;

        public ObservableCollection<TimerSheetViewModel> TimerSheets { get; }

        public CountdownModel Countdown { get; }

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
                    (Countdown.Seconds >= 0 && Countdown.Seconds <= 59) && 
                    (Countdown.Hours != 0 || Countdown.Minutes != 0 || Countdown.Seconds != 0);
            }
        }

        public void ClearOutputFile()
        {
            System.IO.File.WriteAllText(OutputFilename, "");
        }

        public void SaveChanged()
        {

            TimerSheetViewModel tsvm = new TimerSheetViewModel(Countdown.Hours, Countdown.Minutes, Countdown.Seconds, Countdown.Description);
            timer.Tick += tsvm.Tick;
            tsvm.DoRemoveButton = RemoveButton;
            TimerSheets.Add(tsvm);
            Countdown.Hours = 0;
            Countdown.Minutes = 0;
            Countdown.Seconds = 0;
            Countdown.Description = "";
        }

        private void RemoveButton(TimerSheetViewModel viewModel)
        {
            TimerSheets.Remove(viewModel);
        }

        private void AfterTick(ElapsedEventArgs e)
        {
            if (CanSave)
            {
                DoSaveList(TimerSheets.ToArray(), OutputFilename);
            }
        }

        public delegate void SaveList(TimerSheetViewModel[] sheets, string outputfile);
        public SaveList DoSaveList;
    }
}
