using Countdown.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Countdown
{
    /// <summary>
    /// Interaction logic for TimerSheet.xaml
    /// </summary>
    public partial class TimerSheet : UserControl
    {
        private readonly Func<TimeSpan, string> CreateTimeTxt;
        public string ContentText { get; set; }
        private readonly DateTime CountdownStart;
        private readonly TimeSpan CountdownTime;
        public TimerSheet(DateTime Start, TimeSpan Time , Func<TimeSpan, string> createTimeTxt)
        {
            CreateTimeTxt = createTimeTxt;
            CountdownStart = Start;
            CountdownTime = Time;
            InitializeComponent();
        }

        public void SetTimer(string timer)
        {
            Dispatcher.Invoke((Action)(() =>
            {
                Time.Text = timer;
            }));

        }

        internal void Tickhandler(ElapsedEventArgs e)
        {
            TimeSpan timeElapsed = e.SignalTime - CountdownStart;
            TimeSpan timeleft = CountdownTime - timeElapsed;
            SetTimer(CreateTimeTxt(timeleft));
        }

        private T FindParent<T>(DependencyObject dependencyObject) where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(dependencyObject);

            if (parent == null) return null;

            var parentT = parent as T;
            return parentT ?? FindParent<T>(parent);
        }

        private void Close_Clicked(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            var timerSheet = FindParent<TimerSheet>(btn);
            if (timerSheet != null)
            {
                var stackPanel = FindParent<StackPanel>(timerSheet);
                if (stackPanel != null)
                {
                    //Timers.Remove(timerSheet);
                    stackPanel.Children.Remove(timerSheet);
                }
            }



        }

    }
}
