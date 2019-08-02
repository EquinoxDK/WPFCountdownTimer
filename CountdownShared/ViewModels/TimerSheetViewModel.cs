using CountdownShared.Commands;
using CountdownShared.Models;
using System;
using System.Timers;
using System.Windows.Input;

namespace CountdownShared.ViewModels
{
    public class TimerSheetViewModel : IViewModel
    {
        private readonly DateTime start;
        private readonly TimeSpan time;

        public TimerSheetModel TimerSheet { get; }

        public TimerSheetViewModel(int Hours, int Minutes, int Seconds, string Description)
        {
            start = DateTime.Now;
            time = new TimeSpan(Hours, Minutes, Seconds);
            CloseCommand = new CommandResolver<TimerSheetViewModel>(this);
            TimerSheet = new TimerSheetModel { Text = Description, Time = CreateTime(time) };
        }

        private string CreateTime(TimeSpan timeLeft)
        {
            if (timeLeft > TimeSpan.Zero)
            {
                return timeLeft.ToString("hh\\:mm\\:ss");
            }
            else
            {
                return "Done";
            }
        }
        
        public ICommand CloseCommand { get; private set; }

        public delegate void RemoveButton(TimerSheetViewModel viewModel);
        public RemoveButton DoRemoveButton;

        public bool CanUpdate
        {
            get { return true; }
        }

        public void SaveChanged()
        {
            DoRemoveButton(this);
        }

        internal void Tick(ElapsedEventArgs e)
        {
            TimerSheet.Time = CreateTime(time - (e.SignalTime - start));
        }
    }
}
