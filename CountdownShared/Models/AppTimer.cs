namespace CountdownShared.Models
{
    public class AppTimer  
    {
        private System.Timers.Timer TheTimer;
        public event Tickhandler Tick;
        public event Tickhandler AfterTick;
        public delegate void Tickhandler(System.Timers.ElapsedEventArgs e);

        public AppTimer(int interval)
        {
            TheTimer = new System.Timers.Timer
            {
                Interval = interval
            };
            TheTimer.Elapsed += TheTimer_Elapsed;
            TheTimer.AutoReset = true;
            TheTimer.Enabled = true;
        }

        private void TheTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Tick?.Invoke(e);
            AfterTick?.Invoke(e);
        }
    }
}
