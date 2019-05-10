using CountdownMVVM.Commands;
using CountdownMVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;

namespace CountdownMVVM.ViewModels
{
    class TimerSheetViewModel
    {
        private readonly DateTime start;
        private readonly TimeSpan time;
        private readonly TimerSheetModel timerSheet;
        public TimerSheetModel TimerSheet
        {
            get { return timerSheet; }
        }

        public TimerSheetViewModel(int Hours, int Minutes, int Seconds, string Description)
        {
            start = DateTime.Now;
            time = new TimeSpan(Hours, Minutes, Seconds);
            CloseCommand = new TimerSheetViewModelCloseCommand(this);
            timerSheet = new TimerSheetModel { Text = Description, Time = CreateTime(time) };
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

        internal void SaveChanged()
        {
            DoRemoveButton(this);
        }

        internal void Tick(ElapsedEventArgs e)
        {
            timerSheet.Time = CreateTime(time - (e.SignalTime - start));
        }
    }
}
