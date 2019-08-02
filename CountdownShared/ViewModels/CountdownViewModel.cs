using CountdownShared.Commands;
using CountdownShared.Models;
using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Timers;
using System.Windows.Input;
using System.Configuration;

namespace CountdownShared.ViewModels
{
    public class CountdownViewModel : IViewModel
    {
        public CountdownViewModel()
        {
            OutputFilename = ConfigurationManager.AppSettings["Filename"];
            Countdown = new CountdownModel();
            timer = new AppTimer(200);
            timer.AfterTick += AfterTick;
            TimerSheets = new ObservableCollection<TimerSheetViewModel>();
            CreateCommand = new CreateCommand<CountdownViewModel>(this);
            System.IO.File.Create(OutputFilename);
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
            TimerSheets.Remove(viewModel);
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
