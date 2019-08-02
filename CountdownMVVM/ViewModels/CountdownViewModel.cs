using CountdownMVVM.Commands;
using CountdownMVVM.Models;
using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using System.Configuration;

namespace CountdownMVVM.ViewModels
{
    class CountdownViewModel
    {
        public CountdownViewModel()
        {
            countdown = new CountdownModel();
            timer = new AppTimer(200);
            timer.AfterTick += AfterTick;
            timerSheets = new ObservableCollection<TimerSheetViewModel>();
            CreateCommand = new CountdownViewModelCreateCommand(this);

            OutputFilename = ConfigurationManager.AppSettings["Filename"];
            System.IO.File.Create(OutputFilename);

        }

        private readonly AppTimer timer;
        private bool CanSave = true;
        private readonly string OutputFilename;
        private readonly ObservableCollection<TimerSheetViewModel> timerSheets;
        public ObservableCollection<TimerSheetViewModel> TimerSheets
        {
            get { return timerSheets; }
        }

        private readonly CountdownModel countdown;
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

        public void SaveChanges()
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
            if (CanSave)
            {
                SaveList();
            }
        }

        private void SaveList()
        {
            CanSave = false;
            
            StringBuilder sb = new StringBuilder();
            foreach (TimerSheetViewModel item in TimerSheets)
            {
                sb.AppendLine($"{item.TimerSheet.Text} {item.TimerSheet.Time}");
            }
            string content = sb.ToString();

            try
            {
                string oldContent = System.IO.File.ReadAllText(OutputFilename);
                if (!content.Equals(oldContent))
                {
                    System.IO.File.WriteAllText(OutputFilename, content);
                }
            }
            catch (Exception)
            {

            }
            CanSave = true;
        }
    }
}
